namespace ChessGame.Core.Models;

public class Coordinate
{
    public int Column { get; private set; }
    public int Line { get; private set; }
    
    public Coordinate(int line,int column)
    {
        Column = column;
        Line = line;
    }
}