namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class OffsetChessFigure : ChessFigure
{
    protected abstract (int directionColumn, int directionLine)[] Offset { get; }
    
    protected OffsetChessFigure(Color color) : base(color)
    {
    }
    
    public override IEnumerable<PossibleMove> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        List<PossibleMove> moves = new List<PossibleMove>();

        foreach (var offset in Offset)
        {
            var columnPos = fromCoord.Column + offset.directionColumn;
            var linePos = fromCoord.Line + offset.directionLine;
            
            if(!chessBoard.IsInBound(linePos,columnPos)) continue;
            var cell = chessBoard.GetCell(linePos,columnPos);

            if(cell.Figure is not null && cell.Color != Color)
            {
                if(cell.Color != Color)
                    moves.Add(new PossibleMove(cell.Coordinate));
                
                continue;
            }
            
            moves.Add(new PossibleMove(cell.Coordinate));
        }

        return moves;
    }
}