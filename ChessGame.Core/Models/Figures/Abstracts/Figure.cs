namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class Figure
{
    public Position Position { get; protected set; }
    public Color Color { get; init; }
    
    protected Figure(Color color, Position position)
    {
        Color = color;
        Position = position;
    }

    public abstract IEnumerable<Position> GetPossibleMoves(Board board);
}