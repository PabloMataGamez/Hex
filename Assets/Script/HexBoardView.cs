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

        public List<HexPosition> ActivePosition
        {
            set
            {
                foreach (var position in _activePosition)
                {
                    _positionViews[position].Deactivate();
                }

                if (value == null)
                    _activePosition.Clear();
                else
                    _activePosition = value;

                foreach (var position in _activePosition)
                    _positionViews[position].Activate();
            }
        }

        public event EventHandler<PositionEventArgs> PositionClicked;

        private Dictionary<HexPosition, HexPositionView> _positionViews = new Dictionary<HexPosition, HexPositionView>();

        private void OnEnable()
        {
            var positionViews = GetComponentsInChildren<HexPositionView>();
            foreach (var positionView in positionViews)
            {
                _positionViews.Add(positionView.GridPosition, positionView);
            }
        }

        internal void ChilClicked(HexPositionView positionView)
        {
            OnPositionClicked(new PositionEventArgs(positionView.GridPosition));
        }

        protected virtual void OnPositionClicked(PositionEventArgs e)
        {
            var handler = PositionClicked;
            handler.Invoke(this, e);
        }
    }

