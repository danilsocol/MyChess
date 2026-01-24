using ChessGame.Core.Models;

namespace ChessGame.Core;

public class GameEngine : IGameEngine
{
    public GameEngine(ChessBoard board, ChessPlayer whitePlayer, ChessPlayer blackPlayer, ChessPlayer currentTurnPLayer)
    {
        _board = board;
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
        _currentTurnPLayer = currentTurnPLayer;
    }
    
    public GameEngine(ChessPlayer whitePlayer, ChessPlayer blackPlayer)
    {
        _board = new ChessBoard();
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
        _currentTurnPLayer = whitePlayer;
    }

    private readonly ChessBoard _board;
    private readonly ChessPlayer _whitePlayer;
    private readonly ChessPlayer _blackPlayer;
    
    private readonly List<Move> _moveHistory = new();

    private ChessPlayer _currentTurnPLayer;
    public ChessPlayer GetCurrentTurn() => _currentTurnPLayer;
    
    public MoveStatus TryMakeMove(Move move)
    {
        if (!_board.IsInBound(move.From) || !_board.IsInBound(move.To))
        {
            return; // Шаг вне пределов доски ;
        }

        var selectFigure = _board.GetCellFigure(move.From);
        if (selectFigure is null)
        {
            return;
            // Фигура не выбрана
        }

        if (selectFigure.Color != _currentTurnPLayer.Color)
        {
            return;
            // Вы выбрали не свою фигуру
        }

        var possibleMoves = selectFigure.GetPossibleMoves(move.From, _board);
        if (!possibleMoves.Contains(move.To))
        {
            if (move.From == move.To) return; // ход на ту же ячейку

            // не возможно сходить на эту ячейку
        }

        var toCell = _board.GetCell(move.To);
        if (toCell.Figure is not null)
        {
            if (toCell.Figure.Color == _currentTurnPLayer.Color) return; // нельзя рубить свою фигуру
            
            // в этом ходу мы рубим чужую фигуру
        }
        
        //Проверка есть ли шах или мат // проверка поможет ли этот ход
        //Проверка поставили ли шах или мат

        // Срубили?
        // Сделать ход
        // Сменить игрока
    }

    public bool IsGameOver()
    {
        throw new NotImplementedException();
    }
}