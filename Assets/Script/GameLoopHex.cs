using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameLoopHex : MonoBehaviour
{
    [SerializeField] private HexEngine _engine;
    [SerializeField] private HexBoard _hexBoard;

    [SerializeField] private HexBoardView _hexBoardView;

    private StateMachine _stateMachine;

    void Start()
    {
        _hexBoard = new HexBoard();
        // We add the info to what the event should do
        _hexBoard.PieceMoved += (s, e) => e.HexPiece.MoveTo(HexPositionHelper.WorldPosition(e.ToPosition));
        _hexBoard.PieceTaken += (s, e) => e.HexPiece.Taken();   

        _engine = new HexEngine(_hexBoard);
        _hexBoardView = FindObjectOfType<HexBoardView>(true);
      // _hexBoardView.CardDropped += OnCardDropped;
      // _hexBoardView.CardHovered += OnCardHovered;

        //For each visual in the scene we add it to the model and calculate the position they have
     //  var pieceViews = GameObject.FindObjectsOfType<HexPieceView>(true);
     //  foreach (var pieceView in pieceViews)  
     //  {
     //      if (pieceView.Player == Player.Player)
     //          _engine.PlayerPosition = HexPositionHelper.GridPosition(pieceView.transform.position);
     //
     //      _hexBoard.Place(HexPositionHelper.GridPosition(pieceView.WorldPosition), pieceView);
     //  }

        //Initate menu and play state. Start menu state 

      _stateMachine = new StateMachine();
      _stateMachine.Register(States.Menu, new MenuState());
      _stateMachine.Register(States.Playing, new PlayingState(_engine, _hexBoardView));            
      
      _stateMachine.InitialState = States.Menu;
    }

    private void OnCardHovered(object sender, PositionEventArgs e)
    {
        _stateMachine.CurrentState.OnCardHovered();

        //Old code moved to PlayingState
        /*  var highlightPosition = _engine.MoveSets.For(e.CardView.Type).Positions(e.HexPosition); 

          _hexBoardView.ActivePositions = highlightPosition;
        */
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {

        _stateMachine.CurrentState.OnCardDropped();

        //Old code moved to PlayingState

        /*
        _hexBoardView.ActivePositions = new List<HexPosition>();

        Debug.Log(e.HexPosition);
        Debug.Log(e.CardView.Type);

       // _engine.Drop(e.CardView, e.HexPosition);

        if (_engine.Drop(e.CardView, e.HexPosition)) 
        {  
            e.CardView.CardExecuted();
            //_handView.addNewCard();
        }
        */
    }
}