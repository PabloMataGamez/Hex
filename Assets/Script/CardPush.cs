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
        if (!Positions(hoverPosition).Contains(hoverPosition))
            return false;

        /*Foreach (hoverPosition in hoverPositions)
        {
           Vector2 direction = hoverPosition - playerPosition;
           enemyPosition += direction;
        }*/


        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition) //Same as Slash
    {
        var validPositions = new List<HexPosition>();

        if (!HexBoard.TryGetPieceAt(hoverPosition, out var piece))
            validPositions.Add(hoverPosition);

        return validPositions;
    }    
}



