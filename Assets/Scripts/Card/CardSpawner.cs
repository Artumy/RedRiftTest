using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DownloadedImagesData _downloadedImagesData;
    [SerializeField] private GameObject _button;

    private List<Sprite> _sprites = new List<Sprite>();
    public int CountIfCards => _sprites.Count;

    public event Action<Card> OnSpawned;

    private void Awake()
    {
        _sprites = _downloadedImagesData.Sprites;
    }

    public IEnumerator Init()
    {
       yield return StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var cardGameObject = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
            Card card = cardGameObject.GetComponent<Card>();
            card.Initialize(_sprites[i], i);
            OnSpawned?.Invoke(card);
            
            yield return new WaitForSeconds(1f);
        }
        
        _button.SetActive(true);
    }
}
