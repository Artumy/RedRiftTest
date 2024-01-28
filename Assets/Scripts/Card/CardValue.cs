using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _manaText;

    private int _health;
    private int _attack;
    private int _mana;

    public event Action OnDamaged;

    public void Initialize()
    {
        _health = Random.Range(0, 10);
        _attack = Random.Range(0, 10);
        _mana = Random.Range(0, 10);

        _healthText.text = _health.ToString();
        _attackText.text = _attack.ToString();
        _manaText.text = _mana.ToString();
    }

    public IEnumerator ChangeRandomValue()
    {
        int endValue = Random.Range(-2, 10);
        int type = Random.Range(1, 4);
        
        switch (type)
        {
            case 1:
                yield return StartCoroutine(ChangeAttack(endValue));
                break;
            case 2:
                yield return StartCoroutine(ChangeHP(endValue));
                break;
            case 3:
                yield return StartCoroutine(ChangeMana(endValue));
                break;
        }
        
        if(Convert.ToInt32(_healthText.text) < 1)
            OnDamaged?.Invoke();
        
    }

    private IEnumerator ChangeAttack(int endValue)
    {
        int startValue = Convert.ToInt32(_attackText.text);
        int step = startValue - endValue;

        if (step > 0)
            step = -1;
        else
            step = 1;

        for (int i = 0; i < Mathf.Abs(startValue - endValue); i++)
        {
            _attackText.text = (Convert.ToInt32(_attackText.text) + step).ToString();
            yield return new WaitForSeconds(1f);
        }
    }
    
    private IEnumerator ChangeHP(int endValue)
    {
        int startValue = Convert.ToInt32(_healthText.text);
        int step = startValue - endValue;

        if (step > 0)
            step = -1;
        else
            step = 1;

        for (int i = 0; i < Mathf.Abs(startValue - endValue); i++)
        {
            _healthText.text = (Convert.ToInt32(_healthText.text) + step).ToString();
            yield return new WaitForSeconds(1f);
        }
    }
    
    private IEnumerator ChangeMana(int endValue)
    {
        int startValue = Convert.ToInt32(_manaText.text);
        int step = startValue - endValue;

        if (step > 0)
            step = -1;
        else
            step = 1;

        for (int i = 0; i < Mathf.Abs(startValue - endValue); i++)
        {
            _manaText.text = (Convert.ToInt32(_manaText.text) + step).ToString();
            yield return new WaitForSeconds(1f);
        }
    }
    
}
