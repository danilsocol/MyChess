using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models;

public class ChessBoardCell
{
    public const int MaxColumn = 7;  
    public const int MinColumn = 0;  
    public const int MaxLine = 7;  
    public const int MinLine = 0;
    public Coordinate Coordinate { get; init; }
    public Color Color { get; init; }
    public ChessFigure? Figure { get; private set; }
    
    public ChessBoardCell(Coordinate coordinate, Color color)
    {
        if (coordinate.Column < MinColumn || coordinate.Column > MaxColumn || coordinate.Line < MinLine || coordinate.Line > MaxLine)
            throw new ArgumentOutOfRangeException($"Координаты должны быть от 0 до 7");
        
        Coordinate = coordinate;
        Color = color;
    }

    public bool IsEmpty()
    {
        return Figure == null;
    }
}