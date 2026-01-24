namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class ChessFigure
{
    public Color Color { get; init; }
    
    protected ChessFigure(Color color)
    {
        Color = color;
    }

    public abstract IEnumerable<Coordinate> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard);
}