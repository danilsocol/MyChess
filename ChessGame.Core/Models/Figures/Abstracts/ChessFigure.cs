namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class ChessFigure
{
    public Color Color { get; init; }
    
    protected ChessFigure(Color color)
    {
        Color = color;
    }

    public abstract IEnumerable<ChessBoardCell> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard);
}