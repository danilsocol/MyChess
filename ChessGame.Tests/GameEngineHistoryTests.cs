using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§8 ТЗ: история и отмена хода (<c>CORE-HIST-*</c>).</summary>
public class GameEngineHistoryTests
{
    [Fact]
    // CORE-HIST-001 / CORE-STATUS-001: успешный ход и смена позиции.
    public void CORE_HIST_001_MakeMove_ReturnsSuccess_AndChangesTurnAndBoard()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 1, pawn);
        var to = TestDataFactory.C(2, 1);
        var status = engine.MakeMove(new Move(from, to, MoveType.Normal));
        Assert.Equal(MoveStatus.Success, status);
        Assert.Null(board.GetCellFigure(from));
        Assert.NotNull(board.GetCellFigure(to));
        Assert.Equal(Color.Black, engine.GetCurrentTurn().Color);
    }

    [Fact]
    // CORE-HIST-001: история растёт на 1 после успешного хода.
    public void CORE_HIST_001_MakeMove_PushesMoveToHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 1, pawn);
        engine.MakeMove(new Move(from, TestDataFactory.C(2, 1), MoveType.Normal));
        Assert.Single(engine.GetHistory());
    }

    [Fact]
    // CORE-HIST-002: отмена возвращает доску и очередь.
    public void CORE_HIST_002_CancelMove_RestoresBoardAndTurn_AfterSingleMove()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 1, pawn);
        var to = TestDataFactory.C(2, 1);
        engine.MakeMove(new Move(from, to, MoveType.Normal));
        engine.CancelMove();
        Assert.Same(pawn, board.GetCellFigure(from));
        Assert.Null(board.GetCellFigure(to));
        Assert.Equal(Color.White, engine.GetCurrentTurn().Color);
        Assert.Empty(engine.GetHistory());
    }

    [Fact]
    // В истории сохраняется взятая фигура.
    public void MakeMove_Capture_SavesTakenFigureInHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var to = TestDataFactory.C(2, 2);
        var attacker = new Pawn(Color.White, from);
        var victim = new Pawn(Color.Black, to);
        TestDataFactory.PlaceFigure(board, 1, 1, attacker);
        TestDataFactory.PlaceFigure(board, 2, 2, victim);
        var status = engine.MakeMove(new Move(from, to, MoveType.Normal));
        var historyEntry = engine.GetHistory().Peek();
        Assert.Equal(MoveStatus.Success, status);
        Assert.Same(victim, historyEntry.TakenFigure);
    }

    [Fact]
    // CORE-HIST-003: при неуспешной попытке рокировки история не растёт.
    public void CORE_HIST_003_FailedCastling_DoesNotPushHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var before = engine.GetHistory().Count;
        var board = engine.GetBoard();
        var from = TestDataFactory.C(0, 4);
        TestDataFactory.PlaceFigure(board, 0, 4, new King(Color.White));
        engine.MakeMove(new Move(from, TestDataFactory.C(0, 6), MoveType.Castling));
        Assert.Equal(before, engine.GetHistory().Count);
    }

    [Fact]
    // CORE-HIST-004: при неуспешном «спец»-ходе история не растёт.
    public void CORE_HIST_004_FailedSpecialMove_DoesNotPushHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var before = engine.GetHistory().Count;
        var board = engine.GetBoard();
        var from = TestDataFactory.C(4, 4);
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, from));
        engine.MakeMove(new Move(from, TestDataFactory.C(5, 5), MoveType.Promotion));
        Assert.Equal(before, engine.GetHistory().Count);
    }

    [Fact]
    // CORE-HIST-005: успешный ход с типом Promotion увеличивает историю.
    public void CORE_HIST_005_SuccessfulPromotionMove_PushesHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var before = engine.GetHistory().Count;
        var board = engine.GetBoard();
        var from = TestDataFactory.C(6, 0);
        TestDataFactory.PlaceFigure(board, 6, 0, new Pawn(Color.White, from));
        engine.MakeMove(new Move(from, TestDataFactory.C(7, 0), MoveType.Promotion));
        Assert.Equal(before + 1, engine.GetHistory().Count);
    }

    [Fact]
    // CORE-HIST-006: N ходов и N отмен — пустая история.
    public void CORE_HIST_006_TwoMoves_TwoCancels_EmptyHistory()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from1 = TestDataFactory.C(1, 1);
        var from2 = TestDataFactory.C(6, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from1));
        TestDataFactory.PlaceFigure(board, 6, 1, new Pawn(Color.Black, from2));
        engine.MakeMove(new Move(from1, TestDataFactory.C(2, 1), MoveType.Normal));
        engine.MakeMove(new Move(from2, TestDataFactory.C(5, 1), MoveType.Normal));
        engine.CancelMove();
        engine.CancelMove();
        Assert.Empty(engine.GetHistory());
    }
}
