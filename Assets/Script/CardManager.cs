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
    private Canvas _canvasParent;

    private List<GameObject> _cardList;

    private void Awake()
    {
        _canvasParent = FindObjectOfType<Canvas>();

        for (int i = 0; i < 5; i++)
        {
           // AddNewCard();
        }
    }


    void Update()
    {
        // CardsInTable();
    }

    public void AddNewCard(Transform transform)
    {

        if (_totalCards < _maxTotalCards && _cardsInTable < _maxCardsInTable)
        {
            int random = Random.Range(1, 4);

            if (random == 1)
            {
                GameObject newCard = Instantiate(_card1, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
               // _cardList.Add(newCard);
            }
            else if (random == 2)
            {
                GameObject newCard = Instantiate(_card2, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
               // _cardList.Add(newCard);
            }
            else if (random == 3)
            {
                GameObject newCard = Instantiate(_card3, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
               // _cardList.Add(newCard);
            }
            else if (random == 4)
            {
                GameObject newCard = Instantiate(_card4, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity, _canvasParent.transform);
                //_cardList.Add(newCard);
            }
            _totalCards++;
            _cardsInTable++;
        }
    }

    private void CardsInTable() //With the list we created we assign a position to each card
    {
        if (_cardList.Count == 1)
        {
            
        }
        else if (_cardList.Count == 2)
        {

        }
        else if (_cardList.Count == 3)
        {

        }
        else if (_cardList.Count == 4)
        {

        }
        else if (_cardList.Count == 5)
        {

        }
    }
}
