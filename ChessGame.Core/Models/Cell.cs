using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models;

public class Cell
{
    public Position Position { get; init; }
    public Color Color { get; init; }
    public Figure? Figure { get; private set; }
    
    public Cell(Position position, Color color)
    {
        Position = position;
        Color = color;
    }

    public bool IsEmpty()
    {
        return Figure == null;
    }
}