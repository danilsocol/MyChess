using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Rook : DirectionalChessFigure
{
    protected override (int directionColumn, int directionLine)[] Direction { get; } =  
    {
        (0, 1), (0,-1), (1,0), (-1,0)
    };
    
    public Rook(Color color) : base(color)
    {
    }
}