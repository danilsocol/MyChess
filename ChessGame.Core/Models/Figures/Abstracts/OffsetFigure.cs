namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class OffsetFigure : Figure
{
    protected abstract (int directionColumn, int directionLine)[] Offset { get; }
    
    protected OffsetFigure(Color color, Position position) : base(color, position)
    {
    }
    
    public override IEnumerable<Position> GetPossibleMoves(Board board)
    {
        List<Position> moves = new List<Position>();

        foreach (var offset in Offset)
        {
            var columnPos = Position.Column + offset.directionColumn;
            var linePos = Position.Line + offset.directionLine;
            
            if(!board.IsInBound(linePos,columnPos)) continue;
            var cell = board.GetCell(linePos,columnPos);

            if(cell.Figure is not null && cell.Color != Color)
            {
                if(cell.Color != Color)
                    moves.Add(cell.Position);
                
                continue;
            }
            
            moves.Add(cell.Position);
        }

        return moves;
    }
}