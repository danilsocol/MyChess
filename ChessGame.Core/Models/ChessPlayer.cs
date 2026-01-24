namespace ChessGame.Core.Models;

public class ChessPlayer
{
    public ChessPlayer(string nickName, Color color)
    {
        Id = Guid.NewGuid();
        NickName = nickName;
        Color = color;
    }

    public Guid Id { get; init; } 
    public string NickName { get; init; }
    public Color Color { get; init; }
}