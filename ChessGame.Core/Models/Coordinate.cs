namespace ChessGame.Core.Models;

public class Coordinate
{
    protected bool Equals(Coordinate other)
    {
        return Column == other.Column && Line == other.Line;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Coordinate)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Column, Line);
    }

    public int Column { get; init; }
    public int Line { get; init; }
    
    public Coordinate(int line,int column)
    {
        Column = column;
        Line = line;
    }
}