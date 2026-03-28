using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public class King : OffsetChessFigure
{
    protected override (int directionColumn, int directionLine)[] Offset { get; } =
    {
        (1, 1), (-1, -1), (1, -1), (-1, 1),
        (0, 1), (0, -1), (1, 0), (-1, 0)
    };

    protected int[] DirectionColumnForSwap { get; } = [1, -1];

    public King(Color color) : base(color)
    { }
    
    
    public override IEnumerable<PossibleMove> GetPossibleMoves(Coordinate fromCoord, ChessBoard chessBoard)
    {
        var moves = new List<PossibleMove>(base.GetPossibleMoves(fromCoord, chessBoard));
        
        if(!RulesValidator.IsInCheck(Color, chessBoard))
        {
            AddPossibleSwapForDirection(moves, fromCoord, chessBoard, DirectionColumnForSwap[0]);
            AddPossibleSwapForDirection(moves, fromCoord, chessBoard, DirectionColumnForSwap[1]);
        }
        return moves;
    }
    
    
    public IEnumerable<PossibleMove> AddPossibleSwapForDirection(IEnumerable<PossibleMove> moves, Coordinate fromCoord, ChessBoard chessBoard, int direction)
    {
        if (HasMoved) return moves;
        
        var columnPos = fromCoord.Column + direction;
        var linePos = fromCoord.Line;
        
        while (true)
        {
            if(!chessBoard.IsInBound(linePos, columnPos)) break;
            var cell = chessBoard.GetCell(linePos, columnPos);
            
            if(cell.Figure is not null)
            {
                if (cell.Figure is Rook &&
                    cell.Figure.Color == Color &&
                    cell.Figure.HasMoved == false)
                    return moves.Append(new PossibleMove(new Coordinate(fromCoord.Line, fromCoord.Column + direction * 2)));
                else
                    return moves;
            }
            
            if (RulesValidator.IsInCheck(new Coordinate(linePos, columnPos), Color, chessBoard))
            {
                return moves;
            }
            
            columnPos += direction;
        }
        
        return moves;
    }
}