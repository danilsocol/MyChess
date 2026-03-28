using ChessGame.Core;
using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

var whitePlayer = new ChessPlayer("White", Color.White);
var blackPlayer = new ChessPlayer("Black", Color.Black);
IGameEngine engine = new GameEngine(whitePlayer, blackPlayer);

SetupDefaultBoard(engine.GetBoard());

Console.WriteLine("MyChess Console UI");
Console.WriteLine("Type 'help' to see available commands.");
RenderBoard(engine.GetBoard());

while (true)
{
    Console.WriteLine();
    Console.Write($"{engine.GetCurrentTurn().Color}> ");
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exiting game.");
        break;
    }

    if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
    {
        PrintHelp();
        continue;
    }

    if (input.Equals("board", StringComparison.OrdinalIgnoreCase))
    {
        RenderBoard(engine.GetBoard());
        continue;
    }

    if (input.Equals("history", StringComparison.OrdinalIgnoreCase))
    {
        PrintHistory(engine.GetHistory());
        continue;
    }

    if (input.StartsWith("moves ", StringComparison.OrdinalIgnoreCase))
    {
        var payload = input[6..].Trim();
        if (!TryParseCoordinate(payload, out var from))
        {
            Console.WriteLine("Invalid coordinate. Use format like: moves e2");
            continue;
        }

        var cell = engine.GetBoard().GetCell(from);
        if (cell.Figure is null)
        {
            Console.WriteLine("No figure on selected cell.");
            continue;
        }

        var possibleMoves = engine.GetPossibleMoves(cell).ToArray();
        if (possibleMoves.Length == 0)
        {
            Console.WriteLine("No available moves.");
            continue;
        }

        Console.WriteLine(
            $"Possible moves from {ToAlgebraic(from)}: {string.Join(", ", possibleMoves.Select(x => ToAlgebraic(x.To)))}");
        continue;
    }

    if (!TryParseMove(input, out var move))
    {
        Console.WriteLine("Invalid command. Use 'e2 e4' for move or 'help'.");
        continue;
    }

    var moveStatus = engine.MakeMove(move);
    Console.WriteLine(StatusToMessage(moveStatus));

    if (moveStatus == MoveStatus.Success)
    {
        RenderBoard(engine.GetBoard());
        if (RulesValidator.IsInCheck(engine.GetCurrentTurn().Color, engine.GetBoard()))
        {
            Console.WriteLine("Check!");
        }
    }
}

static void PrintHelp()
{
    Console.WriteLine("Available commands:");
    Console.WriteLine("- e2 e4         -> make a move");
    Console.WriteLine("- moves e2      -> show possible moves from cell");
    Console.WriteLine("- board         -> redraw board");
    Console.WriteLine("- history       -> show move history");
    Console.WriteLine("- help          -> show commands");
    Console.WriteLine("- exit          -> quit game");
}

static void PrintHistory(Stack<HistoryMove> history)
{
    if (history.Count == 0)
    {
        Console.WriteLine("Move history is empty.");
        return;
    }

    var ordered = history.Reverse().ToArray();
    for (var i = 0; i < ordered.Length; i++)
    {
        var move = ordered[i];
        var from = ToAlgebraic(move.From);
        var to = ToAlgebraic(move.To);
        var capture = move.TakenFigure is null ? "" : $" x{FigureToChar(move.TakenFigure)}";
        Console.WriteLine($"{i + 1}. {FigureToChar(move.SelectFigure)} {from}->{to}{capture}");
    }
}

static string StatusToMessage(MoveStatus status)
{
    return status switch
    {
        MoveStatus.Success => "Move accepted.",
        MoveStatus.OutOfBounds => "Move is out of board bounds.",
        MoveStatus.NoFigureSelected => "No figure on source cell.",
        MoveStatus.NotYourFigure => "You selected opponent's figure.",
        MoveStatus.SameCell => "Source and destination are the same.",
        MoveStatus.InvalidMove => "This move is not valid for selected figure.",
        MoveStatus.GameOver => "Game is over.",
        _ => $"Unknown status: {status}"
    };
}

static bool TryParseMove(string input, out Move move)
{
    move = new Move(new Coordinate(0, 0), new Coordinate(0, 0), MoveType.Normal);
    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length != 2)
    {
        return false;
    }

    if (!TryParseCoordinate(parts[0], out var from) || !TryParseCoordinate(parts[1], out var to))
    {
        return false;
    }

    move = new Move(from, to, MoveType.Normal);
    return true;
}

static bool TryParseCoordinate(string text, out Coordinate coordinate)
{
    coordinate = new Coordinate(0, 0);
    if (string.IsNullOrWhiteSpace(text) || text.Length != 2)
    {
        return false;
    }

    var file = char.ToLowerInvariant(text[0]);
    var rank = text[1];
    if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
    {
        return false;
    }

    var column = file - 'a';
    var line = rank - '1';
    coordinate = new Coordinate(line, column);
    return true;
}

static string ToAlgebraic(Coordinate coordinate)
{
    var file = (char)('a' + coordinate.Column);
    var rank = (char)('1' + coordinate.Line);
    return $"{file}{rank}";
}

static void RenderBoard(ChessBoard board)
{
    Console.WriteLine();
    Console.WriteLine("  a b c d e f g h");
    for (var line = ChessBoardCell.MaxLine; line >= ChessBoardCell.MinLine; line--)
    {
        Console.Write($"{line + 1} ");
        for (var column = ChessBoardCell.MinColumn; column <= ChessBoardCell.MaxColumn; column++)
        {
            var figure = board.GetCell(line, column).Figure;
            Console.Write(figure is null ? ". " : $"{FigureToChar(figure)} ");
        }
        Console.WriteLine($"{line + 1}");
    }
    Console.WriteLine("  a b c d e f g h");
}

static char FigureToChar(ChessFigure figure)
{
    var symbol = figure switch
    {
        King => 'k',
        Queen => 'q',
        Rook => 'r',
        Bishop => 'b',
        Knight => 'n',
        Pawn => 'p',
        _ => '?'
    };

    return figure.Color == Color.White
        ? char.ToUpperInvariant(symbol)
        : symbol;
}

static void SetupDefaultBoard(ChessBoard board)
{
    for (var line = ChessBoardCell.MinLine; line <= ChessBoardCell.MaxLine; line++)
    {
        for (var column = ChessBoardCell.MinColumn; column <= ChessBoardCell.MaxColumn; column++)
        {
            board.ClearPosition(new Coordinate(line, column));
        }
    }

    SetupBackRank(board, Color.White, 0);
    SetupPawns(board, Color.White, 1);

    SetupBackRank(board, Color.Black, 7);
    SetupPawns(board, Color.Black, 6);
}

static void SetupPawns(ChessBoard board, Color color, int line)
{
    for (var column = ChessBoardCell.MinColumn; column <= ChessBoardCell.MaxColumn; column++)
    {
        var coord = new Coordinate(line, column);
        board.SetFigureAt(coord, new Pawn(color, coord));
    }
}

static void SetupBackRank(ChessBoard board, Color color, int line)
{
    board.SetFigureAt(new Coordinate(line, 0), new Rook(color));
    board.SetFigureAt(new Coordinate(line, 1), new Knight(color));
    board.SetFigureAt(new Coordinate(line, 2), new Bishop(color));
    board.SetFigureAt(new Coordinate(line, 3), new Queen(color, new Coordinate(line, 3)));
    board.SetFigureAt(new Coordinate(line, 4), new King(color));
    board.SetFigureAt(new Coordinate(line, 5), new Bishop(color));
    board.SetFigureAt(new Coordinate(line, 6), new Knight(color));
    board.SetFigureAt(new Coordinate(line, 7), new Rook(color));
}