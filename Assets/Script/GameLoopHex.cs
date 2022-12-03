using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameLoopHex : MonoBehaviour, IDropHandler
{
    void Start()
    {
        var hexBoardView = FindObjectOfType<HexBoardView>();
        hexBoardView.CardDropped += OnCardDropped; //WHAT HAPPENS IF NOT += 
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {
        Debug.Log(e.HexPosition);
        Debug.Log(e.Card);

        //Engine.DropCard(e.HexPositon, e.Card)
    }

    public void OnDrop(PointerEventData e) //NOT SOLVED
    {
        if (e.pointerDrag != null)
        {
         //   _hexBoardView.OnCardDroppedOnChild(, e.pointerDrag.GetComponent<Card>());
        }
    }
}
