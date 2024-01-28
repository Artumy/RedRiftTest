using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    [SerializeField] private CardMover _cardMover;
    [SerializeField] private CardValue _cardValue;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Canvas _canvas;

    private SortingGroup _sortingGroup;
    public event Action<Card> OnDestroyed;
    
    public CardMover CardMover => _cardMover;
    public CardValue CardValue => _cardValue;

    public Canvas Canvas => _canvas;

    private void OnEnable()
    {
        _cardValue.OnDamaged += () => StartCoroutine(Destroy());
    }

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
    }

    private void OnDisable()
    {
        _cardValue.OnDamaged -= () => StartCoroutine(Destroy());
    }

    public void Initialize(Sprite sprite, int sortingGroup)
    {
        _sprite.sprite = sprite;
        _sortingGroup.sortingOrder = sortingGroup;
        _canvas.sortingOrder = sortingGroup;
        _cardValue.Initialize();
    }

    public IEnumerator Destroy()
    {
        Tween tween = transform.DOMove(new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), 3f).SetEase(Ease.Linear);
        Tween tween1 = transform.DORotate(new Vector3(0f, 0f, 0f), 3f);
        
        yield return new WaitWhile(() => tween.IsActive() && tween1.IsActive());
        
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
