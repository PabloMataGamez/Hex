using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlash : CardMoveSet
{
    private int _radius = 1;
    public CardSlash(HexBoard board, HexEngine engine) : base(board, engine)
    {
    }

    public override bool Execute(HexPosition hoverPosition, CardView cardView) //Sames as line, check for every hex and take the piece
    {
        if (!Positions(hoverPosition).Contains(hoverPosition))
            return false;

        /*Foreach (hoverPosition in hoverPositions)
        {
            HexBoard.Take(hoverPosition); 
        }*/

        HexBoard.Take(hoverPosition);

        return true;
    }

    public override List<HexPosition> Positions(HexPosition hoverPosition) //CORRECT?
    {
     // private void CubeScale(HexPosition hexPosition, float factor)
     // return Cube(hexPosition.q * factor, hexPosition.r * factor)

            var validPositions = new List<HexPosition>();
            var currentPosition = HexEngine.PlayerPosition;
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0;  j <= 1; i++)
                {
                   validPositions.Add(currentPosition);
                   currentPosition = new HexPosition(currentPosition.Q, i);
                }

                 if (validPositions.Contains(hoverPosition))
                     return validPositions;
            }
        
        return validPositions;
    }
}

/*
 function cube_scale(hex, factor):
    return Cube(hex.q * factor, hex.r * factor, hex.s * factor)

function cube_ring(center, radius):
    var results = []
    var hex = cube_add(center, cube_scale(cube_direction(4), radius))
    for each 0 <= i < 6:
        for each 0 <= j < radius:
            results.append(hex)
            hex = cube_neighbor(hex, i)
    return results

///////////////////////////////////////////

private void CubeScale(HexPosition hexPosition, float factor)
 return Cube(hexPosition.q * factor, hexPosition.r * factor)

private void CubeRing(center, radius)
{
var validPositions = new List<HexPosition>();
var currentPosition = HexEngine.PlayerPosition;
for(0, <= i, < 6)
{
 for (0, <=j, 1=)
{
validPositions.Add(hexPosition);
hexPosition = nextCube(hexposition, i)

return validPositions;
}
}

}

*/