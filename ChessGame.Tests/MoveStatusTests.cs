using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§7 ТЗ: поведение <c>MakeMove</c> и <c>MoveStatus</c> (<c>CORE-STATUS-*</c>).</summary>
public class MoveStatusTests
{
    [Fact]
    // CORE-STATUS-001: успешный ход возвращает Success (см. также GameEngineHistoryTests).
    public void CORE_STATUS_001_MakeMove_ReturnsSuccess_ForValidPawnMove()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(2, 1), MoveType.Normal));
        Assert.Equal(MoveStatus.Success, status);
    }

    [Fact]
    // CORE-STATUS-002: невалидный ход возвращает код ошибки.
    public void CORE_STATUS_002_MakeMove_ReturnsInvalidMove_WhenIllegal()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, TestDataFactory.C(1, 1)));
        var status = engine.MakeMove(new Move(TestDataFactory.C(1, 1), TestDataFactory.C(4, 1), MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
    }

    [Fact]
    // CORE-STATUS-005: после невалидного хода очередь не меняется.
    public void CORE_STATUS_005_MakeMove_DoesNotChangeTurn_WhenMoveIsInvalid()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(4, 1), MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
        Assert.Equal(Color.White, engine.GetCurrentTurn().Color);
    }

    [Fact]
    // CORE-STATUS-005: после невалидного хода доска не меняется (инвариант позиции).
    public void CORE_STATUS_005b_MakeMove_DoesNotChangeBoard_WhenMoveIsInvalid()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 1, pawn);
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(4, 1), MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
        Assert.Same(pawn, board.GetCellFigure(from));
        Assert.Null(board.GetCellFigure(TestDataFactory.C(4, 1)));
    }

    [Fact]
    // Попытка взять свою фигуру: текущее ядро возвращает InvalidMove (ожидание ТЗ — отдельный статус при доработке ядра).
    public void MakeMove_ReturnsInvalidMove_WhenDestinationHasOwnFigure()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var to = TestDataFactory.C(2, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from));
        TestDataFactory.PlaceFigure(board, 2, 1, new Pawn(Color.White, to));
        var status = engine.MakeMove(new Move(from, to, MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
    }

    [Fact]
    public void MakeMove_DoesNotChangeBoard_WhenOwnCaptureWouldOccur()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        var to = TestDataFactory.C(2, 1);
        var attacker = new Pawn(Color.White, from);
        var ownFigure = new Pawn(Color.White, to);
        TestDataFactory.PlaceFigure(board, 1, 1, attacker);
        TestDataFactory.PlaceFigure(board, 2, 1, ownFigure);
        var status = engine.MakeMove(new Move(from, to, MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
        Assert.Same(attacker, board.GetCellFigure(from));
        Assert.Same(ownFigure, board.GetCellFigure(to));
        Assert.Equal(Color.White, engine.GetCurrentTurn().Color);
    }

    [Fact]
    // CORE-STATUS-003: в enum нет отдельных значений «шах/мат» как результат хода.
    public void CORE_STATUS_003_MoveStatus_DoesNotEncodeCheckmateAsSeparateValue()
    {
        var names = Enum.GetNames<MoveStatus>();
        Assert.DoesNotContain(names, x => x.Contains("Check", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    // CORE-STATUS-004: отдельных статусов «рокировка/en passant» в MoveStatus нет.
    public void CORE_STATUS_004_MoveStatus_DoesNotContainCastlingStatusName()
    {
        var names = Enum.GetNames<MoveStatus>();
        Assert.DoesNotContain(names, x => x.Contains("Castling", StringComparison.OrdinalIgnoreCase));
    }
}
