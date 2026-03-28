using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: ферзь (<c>CORE-QUEEN-*</c>).</summary>
public class QueenMoveGenerationTests
{
    [Fact]
    // CORE-QUEEN-001 (диагональ): взятие на диагонали.
    public void GetPossibleMoves_ContainsEnemyDiagonalCell()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var queen = new Queen(Color.White, from);
        TestDataFactory.PlaceFigure(board, 3, 3, queen);

        // Блокируем остальные 7 направлений.
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.White, TestDataFactory.C(4, 3)));
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.Black, TestDataFactory.C(4, 4)));

        var moves = queen.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 4 && m.To.Column == 4);
    }

    [Fact]
    // CORE-QUEEN-001 (линия): взятие по вертикали.
    public void GetPossibleMoves_ContainsEnemyStraightCell()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var queen = new Queen(Color.White, from);
        TestDataFactory.PlaceFigure(board, 3, 3, queen);

        // Блокируем остальные 7 направлений.
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, TestDataFactory.C(4, 4)));
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.Black, TestDataFactory.C(4, 3)));

        var moves = queen.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 4 && m.To.Column == 3);
    }

    [Fact]
    // CORE-QUEEN-002: клетка за блокирующей фигурой на луче недоступна.
    public void CORE_QUEEN_002_DoesNotContainCellBehindBlocker_OnVerticalRay()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var queen = new Queen(Color.White, from);
        TestDataFactory.PlaceFigure(board, 3, 3, queen);
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.White, TestDataFactory.C(4, 3)));
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.White, TestDataFactory.C(2, 3)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.White, TestDataFactory.C(3, 4)));
        TestDataFactory.PlaceFigure(board, 3, 2, new Pawn(Color.White, TestDataFactory.C(3, 2)));
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, TestDataFactory.C(4, 4)));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.White, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.White, TestDataFactory.C(2, 4)));
        TestDataFactory.PlaceFigure(board, 2, 2, new Pawn(Color.White, TestDataFactory.C(2, 2)));
        var moves = queen.GetPossibleMoves(from, board).ToList();
        Assert.DoesNotContain(moves, m => m.To.Line == 5 && m.To.Column == 3);
    }
}
