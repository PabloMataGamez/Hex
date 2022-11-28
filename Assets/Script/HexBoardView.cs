using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PositionEventArgs : EventArgs
    { 
        public HexPosition HexPosition { get; }

        public PositionEventArgs(HexPosition position)
        {
            HexPosition = position;
        }
    }

    class HexBoardView : MonoBehaviour
    {
        private List<HexPosition> _activePosition = new List<HexPosition>();

        public event EventHandler<PositionEventArgs> PositionClicked; //Event all listener will be evoked

        //We store HexPositionViews using its HexPosition as key/ID
        private Dictionary<HexPosition, HexPositionView> _positionViews = new Dictionary<HexPosition, HexPositionView>();

        public List<HexPosition> ActivePosition
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
            var positionViews = GetComponentsInChildren<HexPositionView>();
            foreach (var positionView in positionViews)
            {//Exception, I have to fix the HexPositionView
                _positionViews.Add(positionView.GridPosition, positionView); //Add all the tiles to the list 
            }
        }

        internal void ChildClicked(HexPositionView positionView) // WHAT DOES THIS DO EXACTLY? IN GAME
        {
            OnPositionClicked(new PositionEventArgs(positionView.GridPosition));
        }

         //WHAT DOES THIS DO EXACTLY? IN GAME
        protected virtual void OnPositionClicked(PositionEventArgs e) // Rising the event, telling to all listener 
        {
            var handler = PositionClicked;
            handler.Invoke(this, e); //Null
        }
    }

