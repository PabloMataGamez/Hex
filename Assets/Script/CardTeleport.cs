using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTeleport : CardMoveSet
{    
    public CardTeleport(HexBoard board, HexEngine engine) : base(board, engine)
    {
    }

    public override bool Execute(HexPosition hoverPosition, CardView cardView)
    {
        if (!Positions(hoverPosition).Contains(hoverPosition))
            return false;

        HexBoard.Move(HexEngine.PlayerPosition, hoverPosition);

        HexEngine.PlayerPosition = hoverPosition;

        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition)
    {
        var validPositions = new List<HexPosition>();
        if (!HexBoard.TryGetPieceAt(hoverPosition, out var piece))
            validPositions.Add(hoverPosition);

        return validPositions;
    }
}