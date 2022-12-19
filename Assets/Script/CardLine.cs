using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLine : CardMoveSet
{
    private static List<Vector2> _directions = new List<Vector2>()
    {
        new Vector2Int(-1, 0), new Vector2Int(+1, 0), new Vector2Int(0, -1), 
        new Vector2Int(+1, -1),new Vector2Int(-1, +1), new Vector2Int(0, +1)
    };

    public CardLine(HexBoard board, HexEngine engine) : base(board, engine)
    { 

    }

    public override bool Execute(HexPosition hoverPosition, CardView cardView)
    {
        if (!Positions(hoverPosition).Contains(hoverPosition))
            return false;

        /*Foreach (hoverPosition in hoverPositions)
        {
            HexBoard.Take(hoverPosition); //HERE, IN LIST OR CREATE LIST IN LOOP AND PASS IT? I 
        }*/                               //I HAVE TO CHECK IF THE HEX HAS A PIECE AND IN THAT CASE TAKE()

        HexBoard.Take(hoverPosition); 

        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition)
    {
        //var currentPosition = HexEngine.PlayerPosition;
        var validPositions = new List<HexPosition>();
       
        foreach (Vector2 direction in _directions)
        {
            var subValidPositions = new List<HexPosition>();
            var currentPosition = HexEngine.PlayerPosition;
            while (HexBoard.IsValid(currentPosition)) //new list each loop
            {
                int qOffset = (int)direction.x;
                int rOffset = (int)direction.y;

                subValidPositions.Add(currentPosition);
                currentPosition = new HexPosition
                    (currentPosition.Q + qOffset, currentPosition.R + rOffset);        
            }

            if (subValidPositions.Contains(hoverPosition))
                return subValidPositions;

            validPositions.AddRange(subValidPositions);
        }  

        return validPositions;
    }   
}
