using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Tests.TestInfrastructure;

namespace ChessGame.Tests;

/// <summary>§9 ТЗ: контракт генерации ходов (<c>CORE-MOVES-*</c>).</summary>
public class PossibleMovesContractTests
{
    [Fact]
    public void CORE_MOVES_001_GetPossibleMoves_ReturnsNonNullCollection()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var king = new King(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, king);
        Assert.NotNull(king.GetPossibleMoves(from, board));
    }

    [Fact]
    public void CORE_MOVES_002_RepeatedCalls_DoNotThrow()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(1, 3);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        _ = pawn.GetPossibleMoves(from, board).ToList();
        _ = pawn.GetPossibleMoves(from, board).ToList();
    }

    [Fact]
    public void CORE_MOVES_003_PawnMoves_HaveUniqueDestinations()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(1, 3);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        var moves = pawn.GetPossibleMoves(from, board).ToList();
        var unique = moves.Select(m => $"{m.To.Line}:{m.To.Column}").Distinct().Count();
        Assert.Equal(unique, moves.Count);
    }

    [Fact]
    public void CORE_MOVES_004_KingMoves_HaveUniqueDestinations()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(3, 3);
        var king = new King(Color.White);
        TestDataFactory.PlaceFigure(board, 3, 3, king);
        var moves = king.GetPossibleMoves(from, board).ToList();
        var unique = moves.Select(m => $"{m.To.Line}:{m.To.Column}").Distinct().Count();
        Assert.Equal(unique, moves.Count);
    }

    [Fact]
    public void CORE_MOVES_005_RepeatedCalls_ReturnEquivalentOrderedDestinations()
    {
        var board = new ChessBoard();
        var from = TestDataFactory.C(1, 3);
        var pawn = new Pawn(Color.White, from);
        TestDataFactory.PlaceFigure(board, 1, 3, pawn);
        var m1 = pawn.GetPossibleMoves(from, board).Select(x => $"{x.To.Line}:{x.To.Column}").OrderBy(x => x).ToArray();
        var m2 = pawn.GetPossibleMoves(from, board).Select(x => $"{x.To.Line}:{x.To.Column}").OrderBy(x => x).ToArray();
        Assert.Equal(m1, m2);
    }
}
