using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameLoopHex : MonoBehaviour
{

    //HexBoard<> _hexBoard;
    void Start()
    {
        //_hexBoard = new HexBoard<TPiece>();

        var hexBoardView = FindObjectOfType<HexBoardView>();
        hexBoardView.CardDropped += OnCardDropped;

        //var pieceViews = GameObject.FindObjectsOfType<PieceView>();
        //foreach (var pieceView in pieceViews)
        //    _HEXboard.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView); WORK HERE REGISTER IN HEXBOARD
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {
        Debug.Log(e.HexPosition);
        Debug.Log(e.Card.Type);
        
        //_engine.Drop(PlayerPosition, e.Card.Type, e.HexPosition); WORK HERE
        
    }
}
