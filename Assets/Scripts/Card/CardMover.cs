using System;
using DG.Tweening;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    private Card _card;
    
    public bool isTable;
    public event Action<Card> OnMouseUped;

    private void Awake()
    {
        isTable = false;
        _card = gameObject.GetComponent<Card>();
    }

    private void OnMouseEnter()
    {
        if(!isTable)
            Scale(new Vector3(1f, 1f, 1f));
    }

    private void OnMouseDrag()
    {
        if(!isTable)
            transform.DOMove(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 10f)), 0f).SetEase(Ease.Linear);
    }

    private void OnMouseExit()
    {
        if(!isTable)
            Scale(new Vector3(0.7f, 0.7f, 0.7f));
    }

    private void OnMouseUp()
    {
        if(!isTable)
            OnMouseUped?.Invoke(_card);
    }

    public void Move(Vector3 position)
    {
        transform.DOMove(position, 1f);
    }

    public void Rotate(Vector3 rotation)
    {
        transform.DORotate(rotation, 1f);
    }

    public void Scale(Vector3 scale)
    {
        transform.DOScale(scale, 1f);
    }
}
