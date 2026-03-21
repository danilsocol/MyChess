namespace ChessGame.Core.Models;

public enum MoveStatus
{
    /// <summary>
    /// Ход успешен
    /// </summary>
    Success,
    /// <summary>
    /// Шаг вне пределов доски
    /// </summary>
    OutOfBounds,
    /// <summary>
    /// Фигура не выбрана
    /// </summary>
    NoFigureSelected,
    /// <summary>
    /// Вы выбрали не свою фигуру
    /// </summary>
    NotYourFigure,
    /// <summary>
    /// Ход на ту же ячейку
    /// </summary>
    SameCell,
    /// <summary>
    /// Невозможно сходить на эту ячейку
    /// </summary>
    InvalidMove,          
    /// <summary>
    /// Нельзя рубить свою фигуру
    /// </summary>
    CannotCaptureOwn,    
    /// <summary>
    /// Игра закончена
    /// </summary>
    GameOver     
}