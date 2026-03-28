using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§4 ТЗ: взятие на проходе (<c>CORE-ENPASSANT-*</c>).</summary>
public class EnPassantRulesTests
{
    [Fact]
    // CORE-ENPASSANT-001: произвольный диагональный ход с типом Promotion не проходит как успешный спецход без позиции en passant.
    public void CORE_ENPASSANT_001_DiagonalJump_IsNotSuccessful_WithoutEnPassantSetup()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(4, 4);
        TestDataFactory.PlaceFigure(board, 4, 4, new Pawn(Color.White, from));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(5, 5), MoveType.Promotion));
        Assert.NotEqual(MoveStatus.Success, status);
    }
}
