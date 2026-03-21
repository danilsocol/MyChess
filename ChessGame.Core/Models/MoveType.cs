namespace ChessGame.Core.Models;

public enum MoveType
{
    /// <summary>
    /// Обычный ход
    /// </summary>
    Normal,
    /// <summary>
    /// Шах
    /// </summary>
    Castling,
    /// <summary>
    /// Свап
    /// </summary>
    Promotion
}