using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§2 ТЗ: валидация входных данных (<c>CORE-VAL-*</c>).</summary>
public class GameEngineInputValidationTests
{
    [Fact]
    // CORE-VAL-001: GetPossibleMoves для пустой клетки — исключение.
    public void CORE_VAL_001_GetPossibleMoves_Throws_WhenCellHasNoFigure()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var emptyCell = engine.GetBoard().GetCell(TestDataFactory.C(3, 3));

        Assert.Throws<ArgumentException>(() => engine.GetPossibleMoves(emptyCell).ToList());
    }

    [Fact]
    // CORE-VAL-005: ход за пределы доски.
    public void CORE_VAL_005_MakeMove_ReturnsOutOfBounds_WhenTargetOutsideBoard()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();

        var status = engine.MakeMove(new Move(TestDataFactory.C(1, 1), TestDataFactory.C(8, 1), MoveType.Normal));

        Assert.Equal(MoveStatus.OutOfBounds, status);
    }

    [Fact]
    // CORE-VAL-002: ход из пустой клетки.
    public void CORE_VAL_002_MakeMove_ReturnsNoFigureSelected_WhenStartCellIsEmpty()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();

        var status = engine.MakeMove(new Move(TestDataFactory.C(1, 1), TestDataFactory.C(2, 1), MoveType.Normal));

        Assert.Equal(MoveStatus.NoFigureSelected, status);
    }

    [Fact]
    // CORE-VAL-003: ход фигурой соперника.
    public void CORE_VAL_003_MakeMove_ReturnsNotYourFigure_WhenTryingToMoveOpponentFigure()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.Black, TestDataFactory.C(1, 1)));

        var status = engine.MakeMove(new Move(TestDataFactory.C(1, 1), TestDataFactory.C(0, 1), MoveType.Normal));

        Assert.Equal(MoveStatus.NotYourFigure, status);
    }

    [Fact]
    // CORE-VAL-004: ход в ту же клетку.
    public void CORE_VAL_004_MakeMove_ReturnsSameCell_WhenMoveStartsAndEndsInSameCell()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(1, 1);
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, from));

        var status = engine.MakeMove(new Move(from, from, MoveType.Normal));

        Assert.Equal(MoveStatus.SameCell, status);
    }

    [Fact]
    // CORE-VAL-002 расширение: нелегальный ход по правилам фигуры.
    public void MakeMove_ReturnsInvalidMove_WhenDestinationNotInPossibleMoves()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        TestDataFactory.PlaceFigure(board, 1, 1, new Pawn(Color.White, TestDataFactory.C(1, 1)));

        var status = engine.MakeMove(new Move(TestDataFactory.C(1, 1), TestDataFactory.C(4, 1), MoveType.Normal));

        Assert.Equal(MoveStatus.InvalidMove, status);
    }

    [Fact]
    // CORE-VAL-006: текущий контракт — null в MakeMove даёт исключение.
    public void CORE_VAL_006_MakeMove_ThrowsNullReferenceException_WhenMoveIsNull()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
#pragma warning disable CS8625
        Assert.Throws<NullReferenceException>(() => engine.MakeMove(null));
#pragma warning restore CS8625
    }

    [Fact]
    // CORE-VAL-007: текущий контракт — CancelMove без истории даёт исключение.
    public void CORE_VAL_007_CancelMove_ThrowsInvalidOperationException_WhenHistoryIsEmpty()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();

        Assert.Throws<InvalidOperationException>(() => engine.CancelMove());
    }
}
