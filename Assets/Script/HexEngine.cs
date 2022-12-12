using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HexEngine   
{
    public HexPosition PlayerPosition { get; set; } //FromPosition will always be PlayerPostion

    private HexBoard _hexBoard;
    private CardMoveSetCollection _cardMoveSetCollection;

   // private Player _currentPlayer = Player.Player1;
    // public Player CurrentPlayer => _currentPlayer;

    public HexEngine(HexBoard board)
    {
        _hexBoard = board;
        _cardMoveSetCollection = new CardMoveSetCollection(_hexBoard); 
    }

    public CardMoveSetCollection MoveSets
    {
        get
        {
            return _cardMoveSetCollection;
        }
    }    
    
    public bool Drop(CardType cardtype, HexPosition dropPosition)
    {    

        if (!_hexBoard.IsValid(dropPosition))
            return false;      

      //  if (piece.Player != CurrentPlayer)
      //      return false;

        if (!MoveSets.TryGetMoveSet(cardtype, out var moveSet))
            return false;

        if (!moveSet.Positions(PlayerPosition).Contains(dropPosition))
            return false;

        if (!moveSet.Execute(PlayerPosition, dropPosition))
            return false;

        return true;
    }    
}

