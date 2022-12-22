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
        var validPositions = Positions(hoverPosition);
        if (!validPositions.Contains(hoverPosition))
            return false;

        foreach (var validPosition in validPositions)
        {
            HexBoard.Take(validPosition);
        }

        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition) //CONTAINS PLAYER POSITION TOO
    {
        var validPositions = new List<HexPosition>();
       
        foreach (Vector2 direction in _directions)
        {
            int qOffset = (int)direction.x;
            int rOffset = (int)direction.y;

            var subValidPositions = new List<HexPosition>();

            var currentPosition = HexEngine.PlayerPosition;
            currentPosition = new HexPosition(currentPosition.Q + qOffset, currentPosition.R + rOffset);
        
            while (HexBoard.IsValid(currentPosition)) //New list each loop
            {  
                subValidPositions.Add(currentPosition);
                currentPosition = new HexPosition(currentPosition.Q + qOffset, currentPosition.R + rOffset);        
            }

            if (subValidPositions.Contains(hoverPosition))
                return subValidPositions;

            validPositions.AddRange(subValidPositions);
        }  

        return validPositions;
    }   
}