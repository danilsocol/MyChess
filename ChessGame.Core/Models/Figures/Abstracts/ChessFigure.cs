namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class ChessFigure
{
    public Color Color { get; init; }
    public bool HasMoved { get; set; } = false;
    
    protected ChessFigure(Color color)
    {
        Color = color;
    }

    public abstract IEnumerable<PossibleMove> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard);
}