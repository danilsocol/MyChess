using ChessGame.Core.Models.Figures;

namespace ChessGame.Core.Models;

public record GameState(
    ChessBoard Board,
    ChessPlayer WhitePlayer,
    ChessPlayer BlackPlayer,
    ChessPlayer CurrentTurnPlayer,
    Stack<HistoryMove> MoveHistory,
    bool IsCheck,
    bool IsCheckmate,
    bool IsStalemate
);