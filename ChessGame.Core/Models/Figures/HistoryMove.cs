using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core.Models.Figures;

public record HistoryMove(Coordinate From, Coordinate To, ChessFigure? TakenFigure) : Move(From, To);
