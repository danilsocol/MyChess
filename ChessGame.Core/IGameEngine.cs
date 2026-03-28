using ChessGame.Core.Models;

namespace ChessGame.Core;

public interface IGameEngine
{
    /// <summary>
    /// Получить текущего игрока
    /// </summary>
    ChessPlayer GetCurrentTurn();
    
    /// <summary>
    /// Получить игровое поле с фигурами
    /// </summary>
    ChessBoard GetBoard();
    
    /// <summary>
    /// Получить историю ходов
    /// </summary>
    Stack<HistoryMove> GetHistory();
    
    /// <summary>
    /// Получить доступных ходы
    /// </summary>
    IEnumerable<PossibleMove> GetPossibleMoves(ChessBoardCell cell);
    
    /// <summary>
    /// Сделать ход 
    /// </summary>
    MoveStatus MakeMove(Move move);
    
    /// <summary>
    /// Отменить прошлый ход
    /// </summary>
    void CancelMove();
    
    /// <summary>
    /// Загрузить игры
    /// </summary>
    void DownloadGame();
    
    /// <summary>
    /// Сохронить игры
    /// </summary>
    void SaveGame();
    
    /// <summary>
    /// Конец игры
    /// </summary>
    void GameOver();
    
    /// <summary>
    /// Экспортировать состояние игры
    /// </summary>
    GameState ExportState();
}
