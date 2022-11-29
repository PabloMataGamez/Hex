using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[SerializeField]
public class ActivationChangeUnityEvent : UnityEvent<bool> { }


public class HexPositionView : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    [SerializeField]
    private ActivationChangeUnityEvent OnActivationChanged;

    private HexBoardView _parent;

    //We transform the world position to grid position
    public HexPosition GridPosition => HexPositionHelper.GridPosition(transform.position); 


    private void Awake()
    {
        gameObject.name = GridPosition.ToString();
    }
    // public event EventHandler Clicked;
    private void Start()
    {
        _parent = GetComponentInParent<HexBoardView>();
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
            _parent.OnCardDroppedOnChild(this, eventData.pointerDrag.GetComponent<Card>());
            
        }
    }
}
