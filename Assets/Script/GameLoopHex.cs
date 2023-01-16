using UnityEngine;

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

      _stateMachine = new StateMachine();
      _stateMachine.Register(States.Menu, new MenuState());
      _stateMachine.Register(States.Playing, new PlayingState(_engine, _hexBoardView));            
      
      _stateMachine.InitialState = States.Menu;
    }

    private void OnCardHovered(object sender, PositionEventArgs e)
    {
        _stateMachine.CurrentState.OnCardHovered();      
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {

        _stateMachine.CurrentState.OnCardDropped();
    }
}