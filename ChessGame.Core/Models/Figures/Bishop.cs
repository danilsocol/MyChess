using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Bishop : DirectionalFigure
{
    protected override (int directionColumn, int directionLine)[] Direction { get; } =  
    {
        (1, 1), (-1, -1), (1, -1), (-1, 1)
    };
    
    public Bishop(Color color, Position position) : base(color, position)
    { }
}