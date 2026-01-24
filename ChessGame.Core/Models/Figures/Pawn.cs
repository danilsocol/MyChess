using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class Pawn : ChessFigure
{
    public bool IsFirstMove { get; set; } = true;
    
    public Pawn(Color color, Coordinate position) : base(color)
    {
    }

    public override IEnumerable<Coordinate> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        List<Coordinate> moves = new List<Coordinate>();
        var direction = Color == Color.White ? 1 : -1;

        if(!chessBoard.IsInBound(fromCoord.Line + direction,fromCoord.Column)) return moves;
        
        var oneStepCell = chessBoard.GetCell(fromCoord.Line + direction,fromCoord.Column);
        if (oneStepCell.IsEmpty())
        {
            moves.Add(oneStepCell.Coordinate);

            if (IsFirstMove && chessBoard.IsInBound(oneStepCell.Coordinate.Line + direction, oneStepCell.Coordinate.Column))
            {
                var twoStepCell = chessBoard.GetCell(oneStepCell.Coordinate.Line + direction, oneStepCell.Coordinate.Column);
                if(twoStepCell.IsEmpty()) moves.Add(twoStepCell.Coordinate);
            }
        }

        foreach (var offset in new int[] {1,-1})
        {
            if(!chessBoard.IsInBound(fromCoord.Line + direction, fromCoord.Column + offset)) continue;
            
            var diagCell = chessBoard.GetCell(fromCoord.Line + direction, fromCoord.Column + offset);
            if (diagCell.Figure != null && diagCell.Figure.Color != Color)
            {
                moves.Add(diagCell.Coordinate); 
            } 
        }

        return moves;
    }
}