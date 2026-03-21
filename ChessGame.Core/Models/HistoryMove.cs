using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models;

public record HistoryMove(ChessFigure SelectFigure, Coordinate From, Coordinate To, MoveType MoveType, ChessFigure? TakenFigure) : Move(From, To, MoveType);
