namespace ChessGame.Core.Models;

public class Coordinate
{
    public int Column { get; init; }
    public int Line { get; init; }
    
    public Coordinate(int line,int column)
    {
        Column = column;
        Line = line;
    }
}