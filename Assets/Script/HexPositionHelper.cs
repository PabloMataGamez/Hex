using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPositionHelper : MonoBehaviour
{
    public const int HexRows = 7;
    public const int HexColumns = 7;
    public const int TileSize = 1;

    public static HexPosition GridPosition(Vector3 worldPosition)
    {
        var scaleWorldPosition = (worldPosition / TileSize);

        var gridPositionQ = (int)(scaleWorldPosition.x + (HexColumns / 2) - 0.5f);
        var gridPositionR = (int)(scaleWorldPosition.z + (HexRows / 2) - 0.5f);

        return new HexPosition(gridPositionQ, gridPositionR);
    }

    public static Vector3 WorldPosition(HexPosition gridPosition)
    {
        var scaledWorldPositionX = gridPosition.Q - (HexColumns / 2) + 0.5f;
        var scaledWorldPositionZ = gridPosition.R - (HexRows / 2) + 0.5f;

        return new Vector3(scaledWorldPositionX, 0, scaledWorldPositionZ) * TileSize;
    }
}
