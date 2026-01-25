namespace ChessGame.Core.Models;

public enum MoveStatus
{
    Success,              // Ход успешен
    OutOfBounds,          // Шаг вне пределов доски
    NoFigureSelected,     // Фигура не выбрана
    NotYourFigure,        // Вы выбрали не свою фигуру
    SameCell,             // Ход на ту же ячейку
    InvalidMove,          // Невозможно сходить на эту ячейку
    CannotCaptureOwn      // Нельзя рубить свою фигуру
}