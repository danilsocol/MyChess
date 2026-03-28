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
    
    private readonly Stack<HistoryMove> _moveHistory = new();

    private ChessPlayer _currentTurnPLayer;
    public ChessPlayer GetCurrentTurn() => _currentTurnPLayer;
    
    private void ChangeTurnPlayer()
    {
        _currentTurnPLayer = _currentTurnPLayer == _whitePlayer ? _blackPlayer : _whitePlayer;
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

    public ChessBoard GetBoard() => _board;

    public Stack<HistoryMove> GetHistory() => _moveHistory;

    public IEnumerable<PossibleMove> GetPossibleMoves(ChessBoardCell cell)
    {
        if (cell.Figure is null) throw new ArgumentException("Выбрана ячейка без фигуры");
        return cell.Figure.GetPossibleMoves(cell.Coordinate, _board);
    }
    
    public MoveStatus MakeMove(Move move)
    {
        if (!_board.IsInBound(move.From) || !_board.IsInBound(move.To))
            return MoveStatus.OutOfBounds;
        
        var selectFigure = _board.GetCellFigure(move.From);
        if (selectFigure is null)
            return MoveStatus.NoFigureSelected;
        
    
        if (selectFigure.Color != _currentTurnPLayer.Color)
            return MoveStatus.NotYourFigure;
        
    
        var possibleMoves = selectFigure.GetPossibleMoves(move.From, _board);
        if (possibleMoves.All(x => !x.To.Equals(move.To)))
        {
            if (move.From == move.To) return MoveStatus.SameCell;
    
            return MoveStatus.InvalidMove;
        }
    
        var toCell = _board.GetCell(move.To);
        ChessFigure? takenFigure = null;
        
        if (toCell.Figure is not null)
        {
            if (toCell.Figure.Color == _currentTurnPLayer.Color) return MoveStatus.InvalidMove;
    
            takenFigure = toCell.Figure;
        }
        
        selectFigure.HasMoved = true;
        _moveHistory.Push(new HistoryMove(selectFigure, move.From, move.To, move.MoveType, takenFigure));
        
        _board.SetFigureAt(move.To, selectFigure);
        _board.ClearPosition(move.From);
        
        ChangeTurnPlayer();
        return MoveStatus.Success;
    }

    public void CancelMove()
    {
        var cancelMove = _moveHistory.Pop();
        
        _board.SetFigureAt(cancelMove.To, cancelMove.TakenFigure);
        _board.SetFigureAt(cancelMove.From, cancelMove.SelectFigure);
        
        ChangeTurnPlayer();
    }

    public void DownloadGame()
    {
        throw new NotImplementedException();
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
    }

    public void GameOver()
    {
        throw new NotImplementedException();
    }
}