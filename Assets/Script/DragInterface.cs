using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image))]
public class DragInterface : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;
    private Camera _camera;

    [SerializeField] 
    private LayerMask _cubeLayer;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    private int _cardsInTable = 0;
    private int _totalCards;

    [SerializeField]
    private int _maxTotalCards = 12;
    [SerializeField]
    private int _maxCardsInTable = 5;

    [SerializeField]
    private GameObject _card1;
    [SerializeField]
    private GameObject _card2;
    [SerializeField]
    private GameObject _card3;
    [SerializeField]
    private GameObject _card4;

    [SerializeField]
    private Canvas _canvasParent;


    private void Awake()
    {
        _camera = Camera.main;
        _canvasParent = FindObjectOfType<Canvas>();

        // AddNewCard();
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
       // m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

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

        if (IsOverCube())
        {
            _cardsInTable--;
            Destroy(this.gameObject);

            AddNewCard();
        }
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

    private bool IsOverCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, _camera.farClipPlane, _cubeLayer))
            return true;
        return false;
    }


    private void AddNewCard()
    {
        if (_totalCards < _maxTotalCards && _cardsInTable < _maxCardsInTable)
        {
            int random = Random.Range(1, 4);

            if (random == 1)
            {
                var newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                    Quaternion.identity, _canvasParent.transform);
            }
            else if (random == 2)
            {
                var newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                    Quaternion.identity, _canvasParent.transform);

            }
            else if (random == 3)
            {
                var newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                    Quaternion.identity, _canvasParent.transform);

            }
            else if (random == 4)
            {
                var newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                    Quaternion.identity, _canvasParent.transform);

            }
            _totalCards++;
            _cardsInTable++;
        }
    }
}