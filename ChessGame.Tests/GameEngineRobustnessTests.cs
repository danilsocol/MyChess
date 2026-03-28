using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§11 ТЗ: устойчивость (<c>CORE-ROBUST-*</c>).</summary>
public class GameEngineRobustnessTests
{
    [Fact]
    public void CORE_ROBUST_001_ManyInvalidMoves_DoNotThrow()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        for (var i = 0; i < 100; i++)
            _ = engine.MakeMove(new Move(TestDataFactory.C(0, 0), TestDataFactory.C(9, 9), MoveType.Normal));
    }

    [Fact]
    public void CORE_ROBUST_002_ValidThenInvalidMove_KeepsEngineUsable()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from));
        _ = engine.MakeMove(new Move(from, TestDataFactory.C(2, 1), MoveType.Normal));
        _ = engine.MakeMove(new Move(TestDataFactory.C(0, 0), TestDataFactory.C(9, 9), MoveType.Normal));
        Assert.NotNull(engine.GetBoard());
    }

    [Fact]
    // CORE-ROBUST-003: серия ходов и отмен до пустой истории.
    public void CORE_ROBUST_003_TwoMovesTwoCancels_EmptyHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var wFrom = TestDataFactory.C(1, 1);
        var bFrom = TestDataFactory.C(6, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, wFrom));
        TestDataFactory.PlaceFigure(board, 6, 1, new Pawn(Color.Black, bFrom));
        _ = engine.MakeMove(new Move(wFrom, TestDataFactory.C(2, 1), MoveType.Normal));
        _ = engine.MakeMove(new Move(bFrom, TestDataFactory.C(5, 1), MoveType.Normal));
        engine.CancelMove();
        engine.CancelMove();
        Assert.Empty(engine.GetHistory());
    }
}
