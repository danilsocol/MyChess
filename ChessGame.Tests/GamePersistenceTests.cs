using ChessGame.Core.Models;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§10 ТЗ: сохранение, загрузка, экспорт (<c>CORE-STATE-*</c>), контракт геттеров (часть INIT-005).</summary>
public class GamePersistenceTests
{
    [Fact]
    // CORE-INIT-005: GetBoard возвращает тот же экземпляр.
    public void CORE_INIT_005_GetBoard_ReturnsSameInstance_OnRepeatedCalls()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.Same(engine.GetBoard(), engine.GetBoard());
    }

    [Fact]
    // CORE-INIT-005: GetHistory возвращает тот же экземпляр.
    public void CORE_INIT_005_GetHistory_ReturnsSameInstance_OnRepeatedCalls()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.Same(engine.GetHistory(), engine.GetHistory());
    }

    [Fact]
    // CORE-STATE-005: ExportState детерминирован при неизменном состоянии.
    public void CORE_STATE_005_ExportState_ReturnsEquivalentData_WhenStateDidNotChange()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var state1 = engine.ExportState();
        var state2 = engine.ExportState();
        Assert.Same(state1.Board, state2.Board);
        Assert.Equal(state1.CurrentTurnPlayer.Color, state2.CurrentTurnPlayer.Color);
        Assert.Equal(state1.MoveHistory.Count, state2.MoveHistory.Count);
        Assert.Equal(state1.IsCheck, state2.IsCheck);
        Assert.Equal(state1.IsCheckmate, state2.IsCheckmate);
        Assert.Equal(state1.IsStalemate, state2.IsStalemate);
    }

    [Fact]
    // CORE-STATE-001: Save/Download пока не реализованы.
    public void CORE_STATE_001_SaveAndDownload_ThrowNotImplementedException()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.Throws<NotImplementedException>(() => engine.SaveGame());
        Assert.Throws<NotImplementedException>(() => engine.DownloadGame());
    }

    [Fact]
    // CORE-STATE-002: история в ExportState не null.
    public void CORE_STATE_002_ExportState_MoveHistory_IsNotNull()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.NotNull(engine.ExportState().MoveHistory);
    }

    [Fact]
    // CORE-STATE-003: ExportState отражает текущего игрока.
    public void CORE_STATE_003_ExportState_PreservesCurrentTurn()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard(Color.Black);
        Assert.Equal(Color.Black, engine.ExportState().CurrentTurnPlayer.Color);
    }

    [Fact]
    // CORE-STATE-004: загрузка через DownloadGame пока не реализована.
    public void CORE_STATE_004_DownloadGame_ThrowsNotImplementedException()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.Throws<NotImplementedException>(() => engine.DownloadGame());
    }

    [Fact]
    public void GameOver_ThrowsNotImplementedException()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        Assert.Throws<NotImplementedException>(() => engine.GameOver());
    }
}
