using System.Collections.Generic;
using UnityEngine;

class PlayingState : State
{
    private HexBoardView _hexBoardView;
    private HexBoard _hexBoard;
    private HexEngine _engine;
    private GameView _playing;

    public PlayingState(HexEngine engine, HexBoardView boardView)
    {
        _engine = engine;
        _hexBoardView = boardView;
    }
    public override void OnEnter()
    {
        base.OnEnter();

        _hexBoard = new HexBoard();

        _hexBoard.PieceMoved += (s, e) => e.HexPiece.MoveTo(HexPositionHelper.WorldPosition(e.ToPosition));
        _hexBoard.PieceTaken += (s, e) => e.HexPiece.Taken();
        _hexBoard.PiecePlaced += (s, e) => e.HexPiece.Placed(HexPositionHelper.WorldPosition(e.ToPosition));

        _engine = new HexEngine(_hexBoard);

        _hexBoardView = GameObject.FindObjectOfType<HexBoardView>(true);
        _playing = GameObject.FindObjectOfType<GameView>(true);

        var pieceViews = GameObject.FindObjectsOfType<HexPieceView>(true);
        foreach (var pieceView in pieceViews)
        {
            if (pieceView.Player == Player.Player)
                _engine.PlayerPosition = HexPositionHelper.GridPosition(pieceView.transform.position);

            _hexBoard.Place(HexPositionHelper.GridPosition(pieceView.WorldPosition), pieceView);
        }

        if (_playing != null)
        {
           _hexBoardView.CardDropped += OnCardDropped;
           _hexBoardView.CardHovered += OnCardHovered;
            _playing.gameObject.SetActive(true);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        if (_hexBoardView != null)
        {
            _hexBoardView.CardDropped -= OnCardDropped;
            _hexBoardView.CardHovered -= OnCardHovered;
        }

        _playing.gameObject.SetActive(false);
        Debug.Log("Game");
    }

    public override void OnSuspend()
    {
        if (_hexBoardView != null)
            _hexBoardView.CardDropped -= OnCardDropped;
    }

    public override void OnResume()
    {
        if (_hexBoardView != null)
            _hexBoardView.CardDropped += OnCardDropped;
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {
        _hexBoardView.ActivePositions = new List<HexPosition>();

        Debug.Log(e.HexPosition);
        Debug.Log(e.CardView.Type);       

        if (_engine.Drop(e.CardView, e.HexPosition))
        {
            e.CardView.CardExecuted();          
        }
    }
    private void OnCardHovered(object sender, PositionEventArgs e)
    {
        var highlightPosition = _engine.MoveSets.For(e.CardView.Type).Positions(e.HexPosition);

        _hexBoardView.ActivePositions = highlightPosition;
    }
}