namespace ChessGame.Core.Models.Figures.Abstracts;

public abstract class DirectionalChessFigure : ChessFigure
{
    protected abstract (int directionColumn, int directionLine)[] Direction { get; }

    protected DirectionalChessFigure(Color color) : base(color)
    {
    }
    
    public override IEnumerable<Coordinate> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        List<Coordinate> moves = new List<Coordinate>();

        foreach (var direction in Direction)
        {
            var columnPos = fromCoord.Column + direction.directionColumn;
            var linePos = fromCoord.Line + direction.directionLine;
            
            while (true)
            {
                if(!chessBoard.IsInBound(linePos, columnPos)) break;
                var cell = chessBoard.GetCell(linePos, columnPos);

                if(cell.Figure is not null)
                {
                    if(cell.Figure.Color != Color)
                        moves.Add(cell.Coordinate);
                    
                    break;
                }
                
                moves.Add(cell.Coordinate);
            }
        }

        return moves;
    }
}