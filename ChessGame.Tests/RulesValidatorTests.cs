using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>Вспомогательные тесты <c>RulesValidator</c> (связаны с §5 ТЗ).</summary>
public class RulesValidatorTests
{
    [Fact]
    // Проверяем, что шах определяется, когда вражеская пешка бьет поле короля.
    public void IsInCheck_ReturnsTrue_WhenKingIsAttackedByPawn()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.Black, TestDataFactory.C(4, 2)));

        var isInCheck = ChessGame.Core.RulesValidator.IsInCheck(Color.White, board);

        Assert.True(isInCheck);
    }

    [Fact]
    // Проверяем, что шах не определяется, если атакующих фигур нет.
    public void IsInCheck_ReturnsFalse_WhenKingIsSafe()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));

        var isInCheck = ChessGame.Core.RulesValidator.IsInCheck(Color.White, board);

        Assert.False(isInCheck);
    }

    [Fact]
    // Проверяем точечную проверку клетки под боем через перегрузку IsInCheck(pos,...).
    public void IsInCheckByPosition_ReturnsTrue_WhenCellIsAttackedByPawn()
    {
        var board = new ChessBoard();
        var target = board.GetCell(3, 1).Coordinate;
        TestDataFactory.PlaceFigure(board, 4, 2, new Pawn(Color.Black, TestDataFactory.C(4, 2)));
        TestDataFactory.PlaceFigure(board, 3, 1, new Pawn(Color.White, target));

        var isInCheck = ChessGame.Core.RulesValidator.IsInCheck(target, Color.White, board);

        Assert.True(isInCheck);
    }

    [Fact]
    // Проверяем базовое условие: без шаха мат невозможен.
    public void IsCheckmate_ReturnsFalse_WhenKingIsNotInCheck()
    {
        var board = new ChessBoard();
        TestDataFactory.PlaceFigure(board, 3, 3, new King(Color.White));

        var isCheckmate = ChessGame.Core.RulesValidator.IsCheckmate(Color.White, board);

        Assert.False(isCheckmate);
    }
}
