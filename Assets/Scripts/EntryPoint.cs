using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private Hand _hand;
    [SerializeField] private Table _table;
    [SerializeField] private GamePlay _gamePlay;
    
    private IEnumerator Start()
    {
        _gamePlay.Initialize(_hand);
        yield return _cardSpawner.Init();
        _table.Init();
    }
    
}
