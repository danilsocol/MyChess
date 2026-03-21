using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;

namespace ChessGame.Core;

public static class RulesValidator
{

    /// <summary>
    /// Проверяет, находится ли король указанного цвета под шахом.
    /// </summary>
    public static bool IsInCheck(Color color, ChessBoard board)
    {
        var posKing = FindKing(color, board);
        if(posKing == null) return false;

        foreach (var cell in board.GameField)
        {
            if(cell.Figure is null || cell.Figure.Color == color) continue;

            var possibleMoves = cell.Figure.GetPossibleMoves(cell.Coordinate, board);
            if(possibleMoves.Any(x => x.To == posKing)) return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Проверяет, находится ли клетка под шахом, другого игрока.
    /// </summary>
    public static bool IsInCheck(Coordinate pos, Color color, ChessBoard board)
    {
        foreach (var cell in board.GameField)
        {
            if(cell.Figure is null || cell.Figure.Color == color) continue;

            var possibleMoves = cell.Figure.GetPossibleMoves(cell.Coordinate, board);
            if(possibleMoves.Any(x => x.To == pos)) return true;
        }
        
        return false;
    }

    /// <summary>
    /// Проверяет, находится ли король указанного цвета под шахом и нет ли возможности сделать ход, чтобы его убрать.
    /// </summary>
    public static bool IsCheckmate(Color color, ChessBoard board)
    {
        if (!IsInCheck(color, board)) return false;

        foreach (var cell in board.GameField) 
        {
            if(cell.Figure is null || cell.Figure.Color == color) continue;
            
            var possibleMoves = cell.Figure.GetPossibleMoves(cell.Coordinate, board);
            foreach (var possibleMove in possibleMoves)
            {
                if (WouldMoveRemoveCheck(cell.Coordinate, possibleMove.To, color, board)) return true;
            }
        }
        return false;
    }
    
    
    private static Coordinate? FindKing(Color color, ChessBoard board)
    {
        foreach (var cell in board.GameField)
        {
            if (cell.Figure is King && cell.Figure.Color == color)
            {
                return cell.Coordinate;
            }
        }

        return null;
    }
    
    /// <summary>
    /// Проверяет, уберёт ли указанный ход шах с короля указанного цвета.
    /// </summary>
    private static bool WouldMoveRemoveCheck(Coordinate from, Coordinate to, Color color, ChessBoard board)
    {
        var originalFigure = board.GetCell(from).Figure;
        var targetFigure = board.GetCell(to).Figure;
        
        board.SetFigureAt(to, originalFigure);
        board.ClearPosition(from);

        bool stillInCheck = IsInCheck(color, board);

        board.SetFigureAt(from, originalFigure);
        board.SetFigureAt(to, targetFigure);

        return !stillInCheck;
    }
}