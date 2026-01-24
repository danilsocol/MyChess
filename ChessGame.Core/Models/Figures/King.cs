using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class King : OffsetChessFigure
{
    protected override (int directionColumn, int directionLine)[] Offset { get; } =
    {
        (1, 1), (-1, -1), (1, -1), (-1, 1),
        (0, 1), (0, -1), (1, 0), (-1, 0)
    };
    
    public King(Color color) : base(color)
    { }
}