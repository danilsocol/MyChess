using ChessGame.Core;
using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Tests.TestInfrastructure;

internal static class TestDataFactory
{
    internal static ChessPlayer WhitePlayer() => new("white", Color.White);

    internal static ChessPlayer BlackPlayer() => new("black", Color.Black);

    internal static GameEngine CreateEngineWithEmptyBoard(Color turnColor = Color.White)
    {
        var board = new ChessBoard();
        var white = WhitePlayer();
        var black = BlackPlayer();
        var turnPlayer = turnColor == Color.White ? white : black;
        return new GameEngine(board, white, black, turnPlayer);
    }

    internal static Coordinate C(int line, int column) => new(line, column);

    internal static void PlaceFigure(ChessBoard board, int line, int column, ChessFigure figure)
    {
        board.SetFigureAt(C(line, column), figure);
    }
}
