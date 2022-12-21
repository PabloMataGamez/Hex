using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPush : CardMoveSet
{
    private int _radius = 1;

    private static List<Vector2Int> _directions = new List<Vector2Int>()
    {
        new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, -1),
        new Vector2Int(-1, 0),new Vector2Int(-1, +1), new Vector2Int(0, +1)
    };

    public CardPush(HexBoard board, HexEngine engine) : base(board, engine)
    {
    }
    
    public override bool Execute(HexPosition hoverPosition, CardView cardView) 
    {
        var validPositions = Positions(hoverPosition);
        if (!validPositions.Contains(hoverPosition))
            return false;

        foreach (var validPosition in validPositions) 
        {
            Vector2Int direction = Vector2Int.zero ;  
             direction.x = hoverPosition.Q - HexEngine.PlayerPosition.Q;
             direction.y = hoverPosition.R - HexEngine.PlayerPosition.R; //Direction

           HexPosition newPosition = new HexPosition
                (validPosition.Q + direction.x, validPosition.R + direction.y); //New position

            if (!HexBoard.IsValid(newPosition)) //Isn't a valid position
                HexBoard.Take(validPosition);

              if(!HexBoard.TryGetPieceAt(newPosition, out var piece))
                HexBoard.Move(validPosition, newPosition);          
        }

        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition)
    {
        var validPositions = new List<HexPosition>();

        Vector2Int initialDirection = _directions[4]; // -1, 0

        var currentPosition = new HexPosition //Offset in corner 4 added
            (HexEngine.PlayerPosition.Q + initialDirection.x,
            HexEngine.PlayerPosition.R + initialDirection.y);  

        for (int i = 0; i < 6; i++)
        {
            Vector2Int direction = _directions[i];
            for (int j = 0; j < _radius; j++)
            {
                validPositions.Add(currentPosition);
                currentPosition = new HexPosition(currentPosition.Q + direction.x, currentPosition.R + direction.y);
            }
        }

        if (validPositions.Contains(hoverPosition)) //REVISE
        {
            List<HexPosition> filteredValidPositions = new List<HexPosition>();
            foreach (var validPosition in validPositions)
            {
                if (validPosition.Equals(hoverPosition) || CheckRange(hoverPosition, validPosition) == 1)
                    filteredValidPositions.Add(validPosition);
            }
            return filteredValidPositions;
        }

        return validPositions;
    }

    private int CheckRange(HexPosition hoverPosition, HexPosition validPosition) //CORRECT? 
    {

        return (Mathf.Abs(hoverPosition.Q - validPosition.Q)
          + Mathf.Abs(hoverPosition.Q + hoverPosition.R - validPosition.Q - validPosition.R)
          + Mathf.Abs(hoverPosition.R - validPosition.R)) / 2;
    }
}