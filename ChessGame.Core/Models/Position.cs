namespace ChessGame.Core.Models;

public class Position
{
    public int Column { get; private set; }
    public int Line { get; private set; }
    
    public Position(int line,int column)
    {
        Column = column;
        Line = line;
    }

    public bool ChangePosition(int line,int column)
    {
        try
        {
            // CheckCorrectPosition(column, line);
            Column = column;
            Line = line;
            
            return true;
        }
        catch (Exception e)
        {
            //todo log
            return false;
        }
    }

    // private bool CheckCorrectPosition(int line,int column)
    // {
    //     if (MaxColumn < column || column < MinColumn)
    //         throw new ArgumentOutOfRangeException($"Номер колонки выходит за допустимое значение (0-7), column: {column}");
    //     
    //     if (MaxLine < line || line < MinLine)
    //         throw new ArgumentOutOfRangeException($"Номер линии выходит за допустимое значение (0-7), line: {line}");
    //
    //     return true;
    // }
}