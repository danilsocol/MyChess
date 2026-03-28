using System.Collections.Generic;
using ChessGame.Core.Models;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

public class GameEngineInitializationTests
{
    [Fact]
    // CORE-INIT-001: базовая целостность поля (размер, ячейки созданы).
    public void CORE_INIT_001_Board_HasExpectedDimensionsAndCells()
    {
        var board = new ChessBoard();
        Assert.Equal(8, board.GameField.GetLength(0));
        Assert.Equal(8, board.GameField.GetLength(1));
        foreach (var cell in board.GameField)
            Assert.NotNull(cell);
    }

    [Fact]
    // CORE-INIT-002: первый ход у белых.
    public void CORE_INIT_002_GetCurrentTurn_DefaultsToWhite_WhenEngineCreatedWithTwoPlayers()
    {
        var engine = new ChessGame.Core.GameEngine(TestDataFactory.WhitePlayer(), TestDataFactory.BlackPlayer());

        var turn = engine.GetCurrentTurn();

        Assert.Equal(Color.White, turn.Color);
    }

    [Fact]
    // CORE-INIT-003: история в начале пустая.
    public void CORE_INIT_003_GetHistory_IsEmpty_WhenEngineCreated()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();

        var history = engine.GetHistory();

        Assert.Empty(history);
    }

    [Fact]
    // CORE-INIT-004: ExportState на старте отражает доску и очередь.
    public void CORE_INIT_004_ExportState_ReturnsSameBoardAndCurrentTurn()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard(Color.Black);

        var state = engine.ExportState();

        Assert.NotNull(state.Board);
        Assert.Equal(Color.Black, state.CurrentTurnPlayer.Color);
    }

    [Fact]
    // CORE-INIT-005 (часть): конструктор из GameState восстанавливает очередь хода.
    public void CORE_INIT_005_Constructor_FromGameState_UsesTurnFromState()
    {
        var board = new ChessBoard();
        var white = TestDataFactory.WhitePlayer();
        var black = TestDataFactory.BlackPlayer();
        var state = new GameState(board, white, black, black, new Stack<HistoryMove>(), false, false, false);
        var engine = new ChessGame.Core.GameEngine(state);

        Assert.Equal(Color.Black, engine.GetCurrentTurn().Color);
    }
}
