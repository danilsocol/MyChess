namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class OffsetChessFigure : ChessFigure
{
    protected abstract (int directionColumn, int directionLine)[] Offset { get; }
    
    protected OffsetChessFigure(Color color) : base(color)
    {
    }
    
    public override IEnumerable<ChessBoardCell> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        List<ChessBoardCell> moves = new List<ChessBoardCell>();

        foreach (var offset in Offset)
        {
            var columnPos = fromCoord.Column + offset.directionColumn;
            var linePos = fromCoord.Line + offset.directionLine;
            
            if(!chessBoard.IsInBound(linePos,columnPos)) continue;
            var cell = chessBoard.GetCell(linePos,columnPos);

            if(cell.Figure is not null && cell.Color != Color)
            {
                if(cell.Color != Color)
                    moves.Add(cell);
                
                continue;
            }
            
            moves.Add(cell);
        }

        return moves;
    }
}