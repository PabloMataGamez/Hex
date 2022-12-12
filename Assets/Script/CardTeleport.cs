using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTeleport : CardMoveSet
{
    public CardTeleport(HexBoard board) : base(board)
    {
    }

    public override bool Execute(HexPosition fromPosition, HexPosition toPosition)
    {
        return base.Execute(fromPosition, toPosition);
    }

    public override List<HexPosition> Positions(HexPosition fromPosition)
    {
        throw new System.NotImplementedException();
    }
}
