using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private Table _table;
    [SerializeField] private HandData _handData;
    
    private int _countOfCards;
    private List<float> _angles = new List<float>();
    private List<Card> _cards = new List<Card>();

    public List<Card> Cards => _cards;

    private void OnEnable()
    {
        _cardSpawner.OnSpawned += Initialize;
        _table.OnTableMoved += RewriteHandCard;
    }

    private void Start()
    {
        _countOfCards = _cardSpawner.CountIfCards;
        FillAnglesList();
    }
    
    private void OnDisable()
    {
        _cardSpawner.OnSpawned -= Initialize;
        _table.OnTableMoved -= RewriteHandCard;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    private Vector3 GetPositionForCard(float angle)
    {
        float x = _handData.Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = _handData.Radius * Mathf.Cos(angle * Mathf.Deg2Rad) + transform.position.y;
        return new Vector3(x, y, 0f);
    }

    private void FillAnglesList()
    {
        _angles.Clear();
        
        float angle = (_handData.DistanceBetweenCard * (_countOfCards - 1)) / 2;
        float startAngle = _handData.StartAngle - angle;

        for (int i = 0; i < _countOfCards; i++)
        {
            _angles.Add(startAngle + _handData.DistanceBetweenCard * i);
        }
    }

    private void Initialize(Card card)
    {
        AddToHand(card);
        MoveToHand(card);
    }

    private void AddToHand(Card card)
    {
        _cards.Add(card);
        card.OnDestroyed += RewriteCardList;
        card.CardMover.OnMouseUped += MoveToHand;
    }

    private void MoveToHand(Card card)
    {
        int index = _cards.IndexOf(card);
        card.CardMover.Move(GetPositionForCard(_angles[index]));
        card.CardMover.Rotate(new Vector3(0f, 0f, -_angles[index]));
    }

    private void RewriteCardList(Card card)
    {
        card.OnDestroyed -= RewriteCardList;
        card.CardMover.OnMouseUped -= MoveToHand;
        _countOfCards--;
        _cards.Remove(card);
        
        Rewrite();
    }

    private void RewriteHandCard(Card card)
    {
        _countOfCards--;
        _cards.Remove(card);
        
        Rewrite();
    }

    private void Rewrite()
    {
        FillAnglesList();
        for (int i = 0; i < _cards.Count; i++)
        {
            MoveToHand(_cards[i]);
        }
    }
}
