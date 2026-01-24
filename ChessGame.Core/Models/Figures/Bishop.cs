using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Bishop : DirectionalChessFigure
{
    protected override (int directionColumn, int directionLine)[] Direction { get; } =  
    {
        (1, 1), (-1, -1), (1, -1), (-1, 1)
    };
    
    public Bishop(Color color) : base(color)
    { }
}