using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[SerializeField]
public class ActivationChangeUnityEvent : UnityEvent<bool> { }


public class HexPositionView : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    [SerializeField]
    private ActivationChangeUnityEvent OnActivationChanged;

    private HexBoardView _parent;

    //HEX: Should be HexPosition
    public HexPosition GridPosition => HexPositionHelper.GridPosition(transform.position);

    // public event EventHandler Clicked;
    private void Start()
    {
        _parent = GetComponentInParent<HexBoardView>();
    }

    public void OnPointerClick(PointerEventData eventData) //Event being listened
    {
        _parent.ChildClicked(this);
    }

    internal void Activate() //Creates an event that allow us to change the material through the editor
    {
        OnActivate?.Invoke();
        OnActivationChanged?.Invoke(true);
    }
    internal void Deactivate()
    {
        OnDeactivate?.Invoke();
        OnActivationChanged?.Invoke(false);
    }

    public void OnDrop(PointerEventData eventData) //IMPLEMENT
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Dropped object was: " + eventData.pointerDrag);
        }
    }
}
