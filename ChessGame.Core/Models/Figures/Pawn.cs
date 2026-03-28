using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Pawn : ChessFigure
{
    public Pawn(Color color, Coordinate position) : base(color)
    {
    }

    public override IEnumerable<PossibleMove> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        List<PossibleMove> moves = new List<PossibleMove>();
        var direction = Color == Color.White ? 1 : -1;

        if(!chessBoard.IsInBound(fromCoord.Line + direction,fromCoord.Column)) return moves;
        
        var oneStepCell = chessBoard.GetCell(fromCoord.Line + direction,fromCoord.Column);
        if (oneStepCell.IsEmpty())
        {
            moves.Add(new PossibleMove(oneStepCell.Coordinate));

            if (!HasMoved && chessBoard.IsInBound(oneStepCell.Coordinate.Line + direction, oneStepCell.Coordinate.Column))
            {
                var twoStepCell = chessBoard.GetCell(oneStepCell.Coordinate.Line + direction, oneStepCell.Coordinate.Column);
                if(twoStepCell.IsEmpty()) moves.Add(new PossibleMove(twoStepCell.Coordinate));
            }
        }

        foreach (var offset in new int[] {1,-1})
        {
            if(!chessBoard.IsInBound(fromCoord.Line + direction, fromCoord.Column + offset)) continue;
            
            var diagCell = chessBoard.GetCell(fromCoord.Line + direction, fromCoord.Column + offset);
            if (diagCell.Figure != null && diagCell.Figure.Color != Color)
            {
                moves.Add(new PossibleMove(diagCell.Coordinate)); 
            } 
        }

        return moves;
    }
}