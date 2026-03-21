using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Knight : OffsetChessFigure
{
    protected override (int directionColumn, int directionLine)[] Offset { get; } =
    {
        (3, 1), (-3, -1), (1, -3), (-1, 3),
        (3, -1), (-3, 1), (-1, -3), (1, 3)
    };
    
    public Knight(Color color) : base(color)
    {
    }
}