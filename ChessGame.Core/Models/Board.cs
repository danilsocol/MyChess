using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models;

public class Board
{
    private const int MaxColumn = 7;  
    private const int MinColumn = 0;  
    private const int MaxLine = 7;  
    private const int MinLine = 0;
    public Cell[,] GameField { get; init; } = new Cell[MaxLine + 1, MaxColumn + 1];

    public Board()
    {
        var rowsCount = GameField.GetUpperBound(0) + 1;
        var columnCount = GameField.Length / rowsCount;

        var color = Color.Black;
        
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                GameField[i, j] = new Cell(new Position(i,j), color);
                color = ChangeColor(color);
            }
        }
    }
    
    public Cell GetCell(Position position)
    {
        var cell = GameField[position.Line, position.Column];
        return cell;
    }
    
    public Cell GetCell(int line,int column)
    {
        var cell = GameField[line, column];
        return cell;
    }
    
    public Figure? GetCellFigure(Position position)
    {
        var cell = GameField[position.Line, position.Column];
        return cell.Figure;
    }
    
    public bool IsEmptyCell(Position position)
    {
        var cell = GameField[position.Line, position.Column];
        return cell.Figure is null;
    }
    
    public bool IsInBound(int line,int column)
    {
        if (MaxColumn < column || column < MinColumn)
            return false;

        if (MaxLine < line || line < MinLine)
            return false;

        return true;
    }
    
    public bool IsInBound(Position position)
    {
        if (MaxColumn < position.Column || position.Column < MinColumn)
            return false;

        if (MaxLine < position.Line || position.Line < MinLine)
            return false;

        return true;
    }

    private Color ChangeColor(Color color)
    {
        if (color == Color.White) color = Color.Black;
        else color = Color.White;

        return color;
    }
}