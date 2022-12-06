using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameLoopHex : MonoBehaviour
{
    [SerializeField]
    private HexEngine _engine;
    [SerializeField]
    private HexBoard _hexBoard;

    
    void Start()
    {
        var hexBoardView = FindObjectOfType<HexBoardView>();
        hexBoardView.CardDropped += OnCardDropped;

        var pieceViews = GameObject.FindObjectsOfType<HexPieceView>();
        foreach (var pieceView in pieceViews)
            if(pieceView.Player == Player ) _engine.PlayerPosition = HexPositionHelper.GridPosition(pieceView.transform.position);
            _hexBoard.Place(HexPositionHelper.GridPosition(pieceView.WorldPosition), pieceView);// WORK HERE REGISTER IN HEXBOARD
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {
        Debug.Log(e.HexPosition);
        Debug.Log(e.Card.Type);
        
        _engine.Drop(e.Card.Type, e.HexPosition);        
    }
}
