using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPush : CardMoveSet
{
    public CardPush(HexBoard board, HexEngine engine) : base(board, engine)
    {
    }
    
    public override bool Execute(HexPosition hoverPosition, CardView cardView)
    {
        throw new System.NotImplementedException();
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition)
    {
        var validPositions = new List<HexPosition>();

        if (!HexBoard.TryGetPieceAt(hoverPosition, out var piece))
            validPositions.Add(hoverPosition);

        return validPositions;
    }    
}

