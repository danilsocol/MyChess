using ChessGame.Core.Models;
using ChessGame.Core.Models.Figures;
using ChessGame.Core.Models.Figures.Abstracts;

namespace ChessGame.Core;

public class GameEngine : IGameEngine
{
    public GameEngine(ChessBoard board, ChessPlayer whitePlayer, ChessPlayer blackPlayer, ChessPlayer currentTurnPLayer)
    {
        _board = board;
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
        _currentTurnPLayer = currentTurnPLayer;
    }
    
    public GameEngine(ChessPlayer whitePlayer, ChessPlayer blackPlayer)
    {
        _board = new ChessBoard();
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
        _currentTurnPLayer = whitePlayer;
    }
    
    public GameEngine(GameState state)
    {
        _board = state.Board;
        _currentTurnPLayer = state.CurrentTurnPlayer;
        _whitePlayer = state.WhitePlayer;
        _blackPlayer = state.BlackPlayer;
        _moveHistory = state.MoveHistory;
    }

    private readonly ChessBoard _board;
    private readonly ChessPlayer _whitePlayer;
    private readonly ChessPlayer _blackPlayer;
    
    private readonly List<HistoryMove> _moveHistory = new();

    private ChessPlayer _currentTurnPLayer;
    public ChessPlayer GetCurrentTurn() => _currentTurnPLayer;
    
    public MoveStatus TryMakeMove(Move move)
    {
        if (!_board.IsInBound(move.From) || !_board.IsInBound(move.To))
        {
            return MoveStatus.OutOfBounds;
        }

        var selectFigure = _board.GetCellFigure(move.From);
        if (selectFigure is null)
        {
            return MoveStatus.NoFigureSelected;
        }

        if (selectFigure.Color != _currentTurnPLayer.Color)
        {
            return MoveStatus.NotYourFigure;
        }

        var possibleMoves = selectFigure.GetPossibleMoves(move.From, _board);
        if (!possibleMoves.Contains(move.To))
        {
            if (move.From == move.To) return MoveStatus.SameCell;

            return MoveStatus.InvalidMove;
        }

        var toCell = _board.GetCell(move.To);
        ChessFigure? takenFigure = null;
        
        if (toCell.Figure is not null)
        {
            if (toCell.Figure.Color == _currentTurnPLayer.Color) return MoveStatus.CannotCaptureOwn;

            takenFigure = toCell.Figure;
        }
        
        _moveHistory.Add(new HistoryMove(move.From,move.To, takenFigure));
        ChangeTurnPlayer();
        return MoveStatus.Success;
    }

    private void ChangeTurnPlayer()
    {
        _currentTurnPLayer = _currentTurnPLayer == _whitePlayer ? _blackPlayer : _whitePlayer;
    }

    public bool IsGameOver()
    {
        throw new NotImplementedException();
    }
    
    public GameState ExportState()
    {
        return new GameState
        (
            _board,
            _whitePlayer,
            _blackPlayer,
            _currentTurnPLayer,
            _moveHistory, 
            false,
            false,
            false
            );
    }

}