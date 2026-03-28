using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§4 ТЗ: рокировка (<c>CORE-CASTLE-*</c>), текущее поведение ядра.</summary>
public class CastlingRulesTests
{
    [Fact]
    // CORE-CASTLE-001: короткая рокировка не выполняется как успешный ход при текущей реализации.
    public void CORE_CASTLE_001_ShortCastling_IsNotSuccessful_WithCurrentEngine()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(0, 4);
        TestDataFactory.PlaceFigure(board, 0, 4, new King(Color.White));
        TestDataFactory.PlaceFigure(board, 0, 7, new Rook(Color.White));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(0, 6), MoveType.Castling));
        Assert.NotEqual(MoveStatus.Success, status);
    }

    [Fact]
    // CORE-CASTLE-002: длинная рокировка не выполняется как успешный ход при текущей реализации.
    public void CORE_CASTLE_002_LongCastling_IsNotSuccessful_WithCurrentEngine()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(0, 4);
        TestDataFactory.PlaceFigure(board, 0, 4, new King(Color.White));
        TestDataFactory.PlaceFigure(board, 0, 0, new Rook(Color.White));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(0, 2), MoveType.Castling));
        Assert.NotEqual(MoveStatus.Success, status);
    }
}
