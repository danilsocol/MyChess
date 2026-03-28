using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§3 ТЗ: конь (<c>CORE-KNIGHT-*</c>).</summary>
public class KnightMoveGenerationTests
{
    [Fact]
    // CORE-KNIGHT-001: классический L-ход.
    public void GetPossibleMoves_ContainsClassicLMove_FromBoardCenter()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var knight = new Knight(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, knight);

        var moves = knight.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 5 && m.To.Column == 4);
    }

    [Fact]
    // CORE-KNIGHT-002: перепрыгивание через фигуры.
    public void GetPossibleMoves_AllowsJumpingOverPieces()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var knight = new Knight(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, knight);

        // Фигуры, которые условно "перекрывают" путь, не должны мешать ходу коня.
        TestDataFactory.PlaceFigure(board, 4, 3, new Pawn(Color.White, TestDataFactory.C(4, 3)));
        TestDataFactory.PlaceFigure(board, 3, 4, new Pawn(Color.Black, TestDataFactory.C(3, 4)));

        var moves = knight.GetPossibleMoves(from, board).ToList();

        Assert.Contains(moves, m => m.To.Line == 5 && m.To.Column == 4);
    }

    [Fact]
    // Проверяем ограничение границ: список ходов не должен содержать клеток вне доски.
    public void GetPossibleMoves_DoesNotContainOutOfBoundsMoves_FromBoardCorner()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(0, 0);
        var knight = new Knight(Color.White);
        TestDataFactory.PlaceFigure(board, 0, 0, knight);

        var moves = knight.GetPossibleMoves(from, board).ToList();

        Assert.DoesNotContain(moves, m => m.To.Line < 0 || m.To.Line > 7 || m.To.Column < 0 || m.To.Column > 7);
    }

    [Fact]
    // CORE-KNIGHT-003: не-L ход через движок отклоняется.
    public void CORE_KNIGHT_003_InvalidKnightMove_ReturnsInvalidMove()
    {
        var engine = TestDataFactory.CreateEngineWithEmptyBoard();
        var board = engine.GetBoard();
        var from = TestDataFactory.C(3, 3);
        TestDataFactory.PlaceFigure(board, 3, 3, new Knight(Color.White));
        var status = engine.MakeMove(new Move(from, TestDataFactory.C(4, 4), MoveType.Normal));
        Assert.Equal(MoveStatus.InvalidMove, status);
    }
}
