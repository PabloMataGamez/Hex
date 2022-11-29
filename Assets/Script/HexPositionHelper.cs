using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPositionHelper : MonoBehaviour // NEED TO BE ADAPTED !!!
{
    public const int HexRows = 7;
    public const int HexColumns = 7;
    public const float TileSize = 0.5f;

    public static HexPosition GridPosition(Vector3 worldPosition) // Pixel to Hex
    {
        var gridPositionQ = Mathf.RoundToInt((Mathf.Sqrt(3) / 3f * worldPosition.x - 1f / 3f * worldPosition.z) / TileSize);
        var gridPositionR = Mathf.RoundToInt(2f/3f * worldPosition.z / TileSize);     

         return new HexPosition(gridPositionQ, gridPositionR);
    }

    public static Vector3 WorldPosition(HexPosition gridPosition) // Hex to Pixel
    {

        var scaledWorldPositionX = TileSize * (Mathf.Sqrt(3) * gridPosition.Q + Mathf.Sqrt(3) / 2f * gridPosition.R);
        var scaledWorldPositionZ = TileSize * (3f / 2f * gridPosition.R);
       // var scaledWorldPositionX = gridPosition.Q - (HexColumns / 2) + 0.5f;
       // var scaledWorldPositionZ = gridPosition.R - (HexRows / 2) + 0.5f;

        return new Vector3(scaledWorldPositionX, 0, scaledWorldPositionZ);
    }
}
