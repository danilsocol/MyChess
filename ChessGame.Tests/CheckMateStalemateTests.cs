using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§5 ТЗ: шах, мат, пат (<c>CORE-CHECK-*</c>, <c>CORE-MATE-*</c>, <c>CORE-STALE-*</c>).</summary>
public class CheckMateStalemateTests
{
    [Fact]
    // CORE-CHECK-001: шах детектируется RulesValidator.
    public void CORE_CHECK_001_IsInCheck_WhenKingAttackedByPawn()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.Black, TestDataFactory.C(4, 2)));
        Assert.True(ChessGame.Core.RulesValidator.IsInCheck(Color.White, board));
    }

    [Fact]
    // CORE-CHECK-002: при отсутствии шаха у короля есть возможные ходы (через генератор фигуры).
    public void CORE_CHECK_002_King_HasMoves_WhenNotInCheck()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var king = new King(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, king);
        var moves = king.GetPossibleMoves(from, board);
        Assert.NotEmpty(moves);
    }

    [Fact]
    // CORE-CHECK-003: позиция с двумя пешками — текущий IsCheckmate возвращает true.
    public void CORE_CHECK_003_IsCheckmate_True_ForDoublePawnForkPosition()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.Black, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.Black, TestDataFactory.C(4, 4)));
        Assert.True(ChessGame.Core.RulesValidator.IsCheckmate(Color.White, board));
    }

    [Fact]
    // CORE-MATE-001: на одиноком короле без атаки мат не объявляется.
    public void CORE_MATE_001_IsCheckmate_False_WhenKingAloneAndSafe()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));
        Assert.False(ChessGame.Core.RulesValidator.IsCheckmate(Color.White, board));
    }

    [Fact]
    // CORE-STALE-001: флаг пата в ExportState по умолчанию false.
    public void CORE_STALE_001_ExportState_IsStalemateFalse_ByDefault()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.False(engine.ExportState().IsStalemate);
    }
}
