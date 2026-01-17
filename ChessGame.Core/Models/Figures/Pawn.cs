using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Pawn : Figure
{
    public bool IsFirstMove { get; set; } = true;
    
    public Pawn(Color color, Position position) : base(color, position)
    {
    }

    public override IEnumerable<Position> GetPossibleMoves(Board board)
    {
        List<Position> moves = new List<Position>();
        var direction = Color == Color.White ? 1 : -1;

        if(!board.IsInBound(Position.Line + direction,Position.Column)) return moves;
        
        var oneStepCell = board.GetCell(Position.Line + direction,Position.Column);
        if (oneStepCell.IsEmpty())
        {
            moves.Add(oneStepCell.Position);

            if (IsFirstMove && board.IsInBound(oneStepCell.Position.Line + direction, oneStepCell.Position.Column))
            {
                var twoStepCell = board.GetCell(oneStepCell.Position.Line + direction, oneStepCell.Position.Column);
                if(twoStepCell.IsEmpty()) moves.Add(twoStepCell.Position);
            }
        }

        foreach (var offset in new int[] {1,-1})
        {
            if(!board.IsInBound(Position.Line + direction, Position.Column + offset)) continue;
            
            var diagCell = board.GetCell(Position.Line + direction, Position.Column + offset);
            if (diagCell.Figure != null && diagCell.Figure.Color != Color)
            {
                moves.Add(diagCell.Position); 
            } 
        }

        return moves;
    }
}