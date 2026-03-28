using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: слон (<c>CORE-BISHOP-*</c>).</summary>
public class BishopMoveGenerationTests
{
    [Fact]
    // CORE-BISHOP-001: диагональное взятие на соседней клетке.
    public void GetPossibleMoves_ContainsEnemyDiagonalCell_WhenEnemyAdjacentOnDiagonal()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var bishop = new Bishop(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, bishop);

        // Блокируем остальные диагонали, чтобы сценарий был детерминирован.
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.Black, TestDataFactory.C(4, 4)));

        var moves = bishop.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 4 && m.To.Column == 4);
    }

    [Fact]
    // CORE-BISHOP-002: нет хода за блокирующую фигуру.
    public void GetPossibleMoves_DoesNotContainCellBehindBlockingPiece()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var bishop = new Bishop(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, bishop);

        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, TestDataFactory.C(4, 4)));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));

        var moves = bishop.GetPossibleMoves(from, board).ToList();

        Assert.DoesNotContain(moves, m => m.To.Line == 5 && m.To.Column == 5);
    }

    [Fact]
    // CORE-BISHOP-003: ортогональный ход не в списке возможных.
    public void CORE_BISHOP_003_NoOrthogonalDestinations_WhenRayBlockedToDiagonalsOnly()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var bishop = new Bishop(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, bishop);
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, TestDataFactory.C(4, 4)));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        var moves = bishop.GetPossibleMoves(from, board).ToList();
        Assert.DoesNotContain(moves, m => m.To.Line == 3 && m.To.Column == 4);
    }
}
