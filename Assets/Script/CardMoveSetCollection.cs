using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardMoveSetCollection 
{
    private Dictionary<CardType, CardMoveSet> _moveSets 
        = new Dictionary<CardType, CardMoveSet>();

    public CardMoveSetCollection(HexBoard board) //TO BE IMPLEMENTED
    {/*
        _moveSets.Add(CardType.Teleport,
            new CardTeleport(
                board                                
                .IsValid()
                ));

        _moveSets.Add(CardType.Slash, 
            new CardSlash(
                board               
                .ValidPositions()
                ));

        _moveSets.Add(CardType.Rotate,
          new CardRotate(
              board             
              .ValidPositions()
              ));


        _moveSets.Add(CardType.Line,
            new CardLine(
                board              
                .ValidPositions()
                ));       
        */
    }

    public ICardMoveSet For(CardType type) 
    => _moveSets[type];


    internal bool TryGetMoveSet(CardType type, out CardMoveSet moveSet)
        => _moveSets.TryGetValue(type, out moveSet);

}