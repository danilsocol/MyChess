namespace ChessGame.Core.Models;

public class PossibleMove
{
    public PossibleMove(Coordinate to, MoveType moveType = MoveType.Normal)
    {
        To = to;
        MoveType = moveType;
    }

    public Coordinate To { get; init; }
    public MoveType MoveType { get; init; }
}