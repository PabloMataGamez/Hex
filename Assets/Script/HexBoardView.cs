using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public HexPosition HexPosition { get; }
    public CardView CardView { get; }

    public PositionEventArgs(HexPosition position, CardView card)
    {
        HexPosition = position;
        CardView = card;
    }
}

public class HexBoardView : MonoBehaviour
{
    private List<HexPosition> _activePosition = new List<HexPosition>();

    public event EventHandler<PositionEventArgs> CardDropped; //Event all listener will be evoked
    public event EventHandler<PositionEventArgs> CardHovered;

    //We store HexPositionViews using its HexPosition as key/ID
    private Dictionary<HexPosition, HexPositionView> _positionViews = new Dictionary<HexPosition, HexPositionView>();

    public List<HexPosition> ActivePositions
    {
        set
        {
            foreach (var position in _activePosition) //First we deactivate whatever was active before
            {
                _positionViews[position].Deactivate();
            }

            if (value == null) //Clear the list
                _activePosition.Clear();
            else
                _activePosition = value;

            foreach (var position in _activePosition) //Now we activate every hex that should be active
                _positionViews[position].Activate();
        }
    }

    private void OnEnable()
    {
        var positionViews = GetComponentsInChildren<HexPositionView>(true);
        foreach (var positionView in positionViews)
        {
            _positionViews.Add(positionView.GridPosition, positionView); //Add all the tiles to the list 
        }
    }

    internal void OnCardDroppedOnChild(HexPositionView positionView, CardView card) // Fires an event and we passed the grid position
    {
        OnCardDropped(new PositionEventArgs(positionView.GridPosition, card)); //Grid position on the tile clicked
    }

    internal void OnCardHoveredOverChild(HexPositionView positionView, CardView card) // Fires an event and we passed the grid position
    {
        OnCardHovered(new PositionEventArgs(positionView.GridPosition, card)); //Grid position on the tile clicked
    }

    protected virtual void OnCardDropped(PositionEventArgs e) // Rising the event, telling to all listener 
    {
        var handler = CardDropped;
        handler?.Invoke(this, e); 
    }

    protected virtual void OnCardHovered(PositionEventArgs e) // Rising the event, telling to all listener 
    {
        var handler = CardHovered;
        handler?.Invoke(this, e); 
    }
}