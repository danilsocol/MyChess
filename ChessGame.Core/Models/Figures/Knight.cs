using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Knight : OffsetChessFigure
{
    protected override (int directionColumn, int directionLine)[] Offset { get; } =
    {
        (2, 1), (-2, -1), (1, -2), (-1, 2),
        (2, -1), (-2, 1), (-1, -2), (1, 2)
    };
    
    public Knight(Color color) : base(color)
    {
    }
}