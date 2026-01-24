using ChessGame.Core.Models;

namespace ChessGame.Core;

public interface IGameEngine
{
    ChessPlayer GetCurrentTurn();
    MoveStatus TryMakeMove(Move move); 
    bool IsGameOver();
}