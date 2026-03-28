using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: король (<c>CORE-KING-*</c>).</summary>
public class KingMoveGenerationTests
{
    [Fact]
    // CORE-KING-001: ход на одну клетку в допустимом направлении.
    public void GetPossibleMoves_ContainsAdjacentCell_FromBoardCenter()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var king = new King(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, king);

        var moves = king.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 4 && m.To.Column == 4);
    }

    [Fact]
    // Проверяем ограничение границ: король не должен получать ходы за пределы доски.
    public void GetPossibleMoves_DoesNotContainOutOfBoundsMoves_FromBoardCorner()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(0, 0);
        var king = new King(Color.White);
        TestDataFactory.PlaceFigure(board, 0, 0, king);

        var moves = king.GetPossibleMoves(from, board).ToList();

        Assert.DoesNotContain(moves, m => m.To.Line < 0 || m.To.Line > 7 || m.To.Column < 0 || m.To.Column > 7);
    }
}
