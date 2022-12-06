using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardMoveSetCollection 
{
    private Dictionary<CardType, CardMoveSet> _moveSets 
        = new Dictionary<CardType, CardMoveSet>();

    public CardMoveSetCollection(HexBoard board)
    {
      /*  _moveSets.Add(CardType.Teleport,
            new ConfigurableMoveSet(
                board,
                (b, p) => new MoveSetHelper<TPiece>(p, b)                
                .ValidPositions()
                ));

        _moveSets.Add(CardType.Slash, 
            new ConfigurableMoveSet(
                board,
                (b, p) => new MoveSetHelper<TPiece>(p, b)                
                .ValidPositions()
                ));

        _moveSets.Add(CardType.Rotate,
          new ConfigurableMoveSet(
              board,
              (b, p) => new MoveSetHelper<TPiece>(p, b)              
              .ValidPositions()
              ));


        _moveSets.Add(CardType.Line,
            new ConfigurableMoveSet(
                board,
                (b, p) => new MoveSetHelper<TPiece>(p, b)                
                .ValidPositions()
                ));        */
    }

    public ICardMoveSet For(CardType type) //For?
    => _moveSets[type];


    internal bool TryGetMoveSet(CardType type, out CardMoveSet moveSet)
        => _moveSets.TryGetValue(type, out moveSet);

}