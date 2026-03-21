using ChessGame.Core.Models;

namespace ChessGame.Core;

public interface IGameEngine
{
    ChessPlayer GetCurrentTurn();
    MoveStatus TryMakeMove(Move move); 
    bool IsGameOver();
}

// Получить доску для отрисовки
// Сделать ход
// Откатить ход
// Конец игры
// Выбрать фигуру и получить доступные ходы
// Сохранить
// Загрузить
// Сдаться