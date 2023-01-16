using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //Card UI behaviour
{   
    [SerializeField]
    private CardType _type;
    public CardType Type => _type;

    public bool dragOnSurfaces = true;
    private Camera _camera;

    [SerializeField] 
    private LayerMask _tileLayer;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    [SerializeField]
    private CardManager _cardManager;

    private void Awake()
    {
        _camera = Camera.main;
        _cardManager = FindObjectOfType<CardManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var card = FindInParents<Image>(gameObject);
        if (card == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(card.transform, false);       

        var image = m_DraggingIcon.AddComponent<Image>();
        image.raycastTarget = false;

        image.sprite = GetComponent<Image>().sprite;

        //Just to make sure its on place
        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = card.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data) //Make the image follow the mouse
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);
    }

    public void CardExecuted()
    {
        _cardManager._cardsInTable--;
        Destroy(this.gameObject);

        _cardManager.AddNewCard(this.transform);
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public bool IsOverTile()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, _camera.farClipPlane, _tileLayer))
            return true;

        return false;
    }   
}