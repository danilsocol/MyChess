using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§4 ТЗ: превращение пешки (<c>CORE-PROMO-*</c>).</summary>
public class PawnPromotionTests
{
    [Fact]
    // CORE-PROMO-001: ход на последнюю горизонталь с MoveType.Promotion принимается движком.
    public void CORE_PROMO_001_LastRankMove_WithPromotionType_Succeeds()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(6, 0);
        TestDataFactory.PlaceFigure(board, 6, 0, new Pawn(Color.White, from));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(7, 0), MoveType.Promotion));
        Assert.Equal(MoveStatus.Success, status);
    }
}
