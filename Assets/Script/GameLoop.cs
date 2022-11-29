using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var hexBoardView = FindObjectOfType<HexBoardView>();
        hexBoardView.CardDropped += OnCardDropped;
    }

    private void OnCardDropped(object sender, PositionEventArgs e)
    {
        Debug.Log(e.HexPosition);
        Debug.Log(e.Card);

        //Engine.DropCArd(e.HewPositon, e.Card)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
