using System;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Hand _hand;

    private List<Vector3> _cardPosition = new List<Vector3>();
    private List<Card> _cards = new List<Card>();

    public event Action<Card> OnTableMoved;

    public void Init()
    {
        for (int i = 0; i < _hand.Cards.Count; i++)
        {
            _hand.Cards[i].CardMover.OnMouseUped += MoveToTable;
        }
        FillCardPosition();
    }

    private void MoveToTable(Card card)
    {
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Table table))
            {
                _cards.Add(card);
                card.CardMover.Move(_cardPosition[_cards.Count - 1]);
                card.CardMover.Rotate(new Vector3(0f, 0f, 0f));
                card.CardMover.Scale(new Vector3(0.9f, 0.9f, 0.9f));
                card.CardMover.isTable = true;
                OnTableMoved?.Invoke(card);
                card.CardMover.OnMouseUped -= MoveToTable;
            }
        }
    }

    private void FillCardPosition()
    {
        _cardPosition.Add(new Vector3(-1.2f, transform.position.y, transform.position.z));
        _cardPosition.Add(new Vector3(1.2f, transform.position.y, transform.position.z));
        _cardPosition.Add(new Vector3(-3.7f, transform.position.y, transform.position.z));
        _cardPosition.Add(new Vector3(3.7f, transform.position.y, transform.position.z));
        _cardPosition.Add(new Vector3(-6.2f, transform.position.y, transform.position.z));
        _cardPosition.Add(new Vector3(6.2f, transform.position.y, transform.position.z));
    }
}