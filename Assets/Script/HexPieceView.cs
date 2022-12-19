using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexPieceView : MonoBehaviour
{
    [SerializeField]
    private Player _player;    

    public Player Player => _player;    

    public Vector3 WorldPosition => transform.position;

    private void Start()
    {
        var gridPostition = HexPositionHelper.GridPosition(transform.position);
        transform.position = HexPositionHelper.WorldPosition(gridPostition);
    }

    internal void MoveTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    internal void Taken()
    {
        gameObject.SetActive(false);
    }

    internal void Placed(Vector3 worldPosition) 
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }
}
