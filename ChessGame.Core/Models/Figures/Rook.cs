using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Rook : DirectionalFigure
{
    protected override (int directionColumn, int directionLine)[] Direction { get; } =  
    {
        (0, 1), (0,-1), (1,0), (-1,0)
    };
    
    public Rook(Color color, Position position) : base(color, position)
    {
    }
}