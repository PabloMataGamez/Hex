using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour 
{
    public int _cardsInTable = 0;
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
    private GameObject _card5;

    [SerializeField]
    private Canvas _canvasParent;

    private List<GameObject> _cardList; 

    private void Awake()
    {
        _cardList = new List<GameObject>();
        _canvasParent = FindObjectOfType<Canvas>();      
    }  

    public void AddNewCard(Transform transform)
    {
        if (_totalCards < _maxTotalCards && _cardsInTable < _maxCardsInTable)
        {
            int random = Random.Range(1, 5);

            if (random == 1)
            {
                GameObject newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
            }
            else if (random == 2)
            {
                GameObject newCard = Instantiate(_card2, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
            }
            else if (random == 3)
            {
                GameObject newCard = Instantiate(_card3, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
            }
            else if (random == 4)
            {
                GameObject newCard = Instantiate(_card4, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
            }
            else if (random == 5)
            {
                GameObject newCard = Instantiate(_card5, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
            }
            _totalCards++;
            _cardsInTable++;
        }
    }
}
