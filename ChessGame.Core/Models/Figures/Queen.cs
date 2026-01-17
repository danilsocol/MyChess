using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Queen : DirectionalFigure
{
    protected override (int directionColumn, int directionLine)[] Direction { get; } =  
    {
        (1, 1), (-1,-1), (1,-1), (-1,1),
        (0, 1), (0,-1), (1,0), (-1,0)
    };
    
    public Queen(Color color, Position position) : base(color, position)
    { }
}