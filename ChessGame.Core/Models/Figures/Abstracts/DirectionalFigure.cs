namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class DirectionalFigure : Figure
{
    protected abstract (int directionColumn, int directionLine)[] Direction { get; }

    protected DirectionalFigure(Color color, Position position) : base(color, position)
    {
    }
    
    public override IEnumerable<Position> GetPossibleMoves(Board board)
    {
        List<Position> moves = new List<Position>();

        foreach (var direction in Direction)
        {
            var columnPos = Position.Column + direction.directionColumn;
            var linePos = Position.Line + direction.directionLine;
            
            while (true)
            {
                if(!board.IsInBound(linePos, columnPos)) break;
                var cell = board.GetCell(linePos, columnPos);

                if(cell.Figure is not null)
                {
                    if(cell.Figure.Color != Color)
                        moves.Add(cell.Position);
                    
                    break;
                }
                
                moves.Add(cell.Position);
            }
        }

        return moves;
    }
}