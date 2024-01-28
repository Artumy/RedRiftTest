using System.Collections;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private Hand _hand;

    public void Initialize(Hand hand)
    {
        _hand = hand;
    }
    
    public void StartGame()
    {
        StartCoroutine(StartChangeValue());
    }

    private IEnumerator StartChangeValue()
    {
        while (_hand.Cards.Count != 0)
        {
            for (int i = 0; i < _hand.Cards.Count; i++)
            {
                yield return StartCoroutine(_hand.Cards[i].CardValue.ChangeRandomValue());
            }
        }
    }
}
