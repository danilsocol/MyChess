using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models;

public class ChessBoard
{
    public ChessBoardCell[,] GameField { get; init; } = new ChessBoardCell[ChessBoardCell.MaxLine + 1, ChessBoardCell.MaxColumn + 1];

    public ChessBoard()
    {
        var rowsCount = GameField.GetUpperBound(0) + 1;
        var columnCount = GameField.Length / rowsCount;

        var color = Color.Black;
        
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                GameField[i, j] = new ChessBoardCell(new Coordinate(i,j), color);
                color = ChangeColor(color);
            }
        }
    }
    
    public ChessBoardCell GetCell(Coordinate position)
    {
        var cell = GameField[position.Line, position.Column];
        return cell;
    }
    
    public ChessBoardCell GetCell(int line,int column)
    {
        var cell = GameField[line, column];
        return cell;
    }
    
    public ChessFigure? GetCellFigure(Coordinate position)
    {
        var cell = GameField[position.Line, position.Column];
        return cell.Figure;
    }
    
    public bool IsInBound(Coordinate coordinate) => IsInBound(coordinate.Line, coordinate.Column);
    
    public bool IsInBound(int line,int column)
    {
        if (ChessBoardCell.MaxColumn < column || column < ChessBoardCell.MinColumn)
            return false;

        if (ChessBoardCell.MaxLine < line || line < ChessBoardCell.MinLine)
            return false;

        return true;
    }

    public void SetFigureAt(Coordinate coordinate, ChessFigure figure)
    {
        
    }

    public void ClearPosition(Coordinate position)
    {
        
    }

    private Color ChangeColor(Color color)
    {
        if (color == Color.White) color = Color.Black;
        else color = Color.White;

        return color;
    }
}