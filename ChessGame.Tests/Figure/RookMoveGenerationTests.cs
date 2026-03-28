using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: ладья (<c>CORE-ROOK-*</c>).</summary>
public class RookMoveGenerationTests
{
    [Fact]
    // CORE-ROOK-001: взятие по вертикали на соседней клетке.
    public void GetPossibleMoves_ContainsEnemyStraightCell_WhenEnemyAdjacentVertically()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var rook = new Rook(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, rook);

        // Блокируем остальные направления.
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.Black, TestDataFactory.C(4, 3)));

        var moves = rook.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 4 && m.To.Column == 3);
    }

    [Fact]
    // CORE-ROOK-002: нет хода за блокирующую фигуру.
    public void GetPossibleMoves_DoesNotContainCellBehindBlockingPiece()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var rook = new Rook(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, rook);

        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.White, TestDataFactory.C(4, 3)));
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));

        var moves = rook.GetPossibleMoves(from, board).ToList();

        Assert.DoesNotContain(moves, m => m.To.Line == 5 && m.To.Column == 3);
    }

    [Fact]
    // CORE-ROOK-003: диагональ не в списке ходов.
    public void CORE_ROOK_003_NoDiagonalDestinations_WhenOrthogonalRaysBlocked()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var rook = new Rook(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, rook);
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.White, TestDataFactory.C(4, 3)));
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));
        var moves = rook.GetPossibleMoves(from, board).ToList();
        Assert.DoesNotContain(moves, m => m.To.Line == 4 && m.To.Column == 4);
    }
}
