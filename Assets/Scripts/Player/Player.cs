using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerSkills))]
public class Player : MonoBehaviour
{
    [SerializeField] private Collider2D _target;

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<float, float> IntoxicationChanged;
    public event UnityAction<float> ScoreChanged;
    public event UnityAction MoneyTaked;
    public event UnityAction MoneyLosed;
    public event UnityAction WeedTaked;
    public event UnityAction WeedLosed;

    private PlayerSkills _skill;
    private float _minIntoxication = 0;
    private float _percent = 100;
    private float _detoxicationPower;
    private WaitForSeconds _waitingTime = new WaitForSeconds(0.2f);

    public float Score { get; private set; }
    public float CurrentIntoxication { get; private set; }
    public int  Money { get; private set; }

    public Collider2D Target => _target;

    private void Start()
    {
        _skill = GetComponent<PlayerSkills>();
        StartCoroutine(Detoxication());
        StartCoroutine(IncreaseScore());
    }

    public void PickUpWeed(float weedPower)
    {
        CurrentIntoxication = Mathf.Clamp(CurrentIntoxication + weedPower, _minIntoxication, _skill.MaxIntoxication);
        IntoxicationChanged?.Invoke(CurrentIntoxication, _percent);
        WeedTaked?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        CurrentIntoxication -= damage;
        IntoxicationChanged?.Invoke(CurrentIntoxication, _percent);   
        WeedLosed?.Invoke();
    }

    public void MeetPolice(int multiplier, int fine)
    {
        CurrentIntoxication = CurrentIntoxication / multiplier;
        Money = Mathf.Clamp(Money - fine, 0, 9999999);
        MoneyChanged?.Invoke(Money);
        WeedLosed?.Invoke();
        MoneyLosed?.Invoke();
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
        MoneyTaked?.Invoke();
    }

    public void SpendMoney(int money)
    {
        Money -= money;
        MoneyChanged?.Invoke(money);
    }

    public void SpendScore(float value)
    {
        Score -= value;
    }

    private IEnumerator Detoxication()
    {
        while(true)
        {
            CurrentIntoxication = Mathf.Clamp(CurrentIntoxication - _detoxicationPower, _minIntoxication, _skill.MaxIntoxication);
            IntoxicationChanged?.Invoke(CurrentIntoxication, _percent);

            yield return _waitingTime;  
        }
    }

    private IEnumerator IncreaseScore()
    {
        while(true)
        {
            Score += _skill.ScorePerTime * CurrentIntoxication / _skill.MaxIntoxication;
            ScoreChanged?.Invoke(Score);

            yield return _waitingTime;
        }
    }
}
