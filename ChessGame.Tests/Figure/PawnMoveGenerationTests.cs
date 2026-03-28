using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: пешка (<c>CORE-PAWN-*</c>).</summary>
public class PawnMoveGenerationTests
{
    [Fact]
    // CORE-PAWN-001 и CORE-PAWN-002: один и два шага вперёд с начальной позиции.
    public void GetPossibleMoves_ReturnsOneAndTwoStepForward_OnFirstMove()
    {
        var board = new ChessBoard();
        var pawn = new Pawn(Color.White, TestDataFactory.C(1, 3));
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);

        var moves = pawn.GetPossibleMoves(TestDataFactory.C(1, 3), board).ToList();

        Assert.Contains(moves, m => m.To.Line == 2 && m.To.Column == 3);
        Assert.Contains(moves, m => m.To.Line == 3 && m.To.Column == 3);
    }

    [Fact]
    // CORE-PAWN-004: блокировка впереди запрещает ход вперёд.
    public void GetPossibleMoves_DoesNotReturnForwardMove_WhenBlocked()
    {
        var board = new ChessBoard();
        var pawn = new Pawn(Color.White, TestDataFactory.C(1, 3));
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.Black, TestDataFactory.C(2, 3)));

        var moves = pawn.GetPossibleMoves(TestDataFactory.C(1, 3), board).ToList();

        Assert.DoesNotContain(moves, m => m.To.Line == 2 && m.To.Column == 3);
        Assert.DoesNotContain(moves, m => m.To.Line == 3 && m.To.Column == 3);
    }

    [Fact]
    // CORE-PAWN-005: диагональное взятие.
    public void GetPossibleMoves_ReturnsDiagonalCapture_WhenEnemyOnDiagonal()
    {
        var board = new ChessBoard();
        var pawn = new Pawn(Color.White, TestDataFactory.C(1, 3));
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        TestDataFactory.PlaceFigure(board, 2, 4, new Pawn(Color.Black, TestDataFactory.C(2, 4)));

        var moves = pawn.GetPossibleMoves(TestDataFactory.C(1, 3), board).ToList();

        Assert.Contains(moves, m => m.To.Line == 2 && m.To.Column == 4);
    }

    [Fact]
    // CORE-PAWN-003: после первого хода двойной шаг недоступен.
    public void CORE_PAWN_003_NoTwoStep_AfterFirstMoveFlagDisabled()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(1, 3);
        var pawn = new Pawn(Color.White, from) { HasMoved = true };
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        var moves = pawn.GetPossibleMoves(from, board).ToList();
        Assert.Contains(moves, m => m.To.Line == 2 && m.To.Column == 3);
        Assert.DoesNotContain(moves, m => m.To.Line == 3 && m.To.Column == 3);
    }

    [Fact]
    // CORE-PAWN-007: ход назад недопустим.
    public void CORE_PAWN_007_NoBackwardMoves()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 3, 3, pawn);
        var moves = pawn.GetPossibleMoves(from, board).ToList();
        Assert.DoesNotContain(moves, m => m.To.Line < from.Line);
    }

    [Fact]
    // CORE-PAWN-006: взятие по прямой недопустимо.
    public void CORE_PAWN_006_NoForwardCapture_WhenEnemyStraightAhead()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(1, 3);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        TestDataFactory.PlaceFigure(board, 2, 3, new Pawn(Color.Black, TestDataFactory.C(2, 3)));
        var moves = pawn.GetPossibleMoves(from, board).ToList();
        Assert.DoesNotContain(moves, m => m.To.Line == 2 && m.To.Column == 3);
    }
}
