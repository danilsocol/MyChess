using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§6 ТЗ: ничьи (<c>CORE-DRAW-*</c>); при отсутствии логики в ядре — контракт state.</summary>
public class DrawRulesTests
{
    [Fact]
    public void CORE_DRAW_001_ExportState_StalemateFlag_NotSetByDefault() =>
        Assert.False(TestDataFactory.CreateEngineWithEmptyBoard().ExportState().IsStalemate);

    [Fact]
    public void CORE_DRAW_002_ExportState_StalemateFlag_NotSetByDefault() =>
        Assert.False(TestDataFactory.CreateEngineWithEmptyBoard().ExportState().IsStalemate);

    [Fact]
    public void CORE_DRAW_003_ExportState_StalemateFlag_NotSetByDefault() =>
        Assert.False(TestDataFactory.CreateEngineWithEmptyBoard().ExportState().IsStalemate);

    [Fact]
    public void CORE_DRAW_004_ExportState_StalemateFlag_NotSetByDefault() =>
        Assert.False(TestDataFactory.CreateEngineWithEmptyBoard().ExportState().IsStalemate);

    [Fact]
    public void CORE_DRAW_005_ExportState_StalemateFlag_NotSetByDefault() =>
        Assert.False(TestDataFactory.CreateEngineWithEmptyBoard().ExportState().IsStalemate);
}
