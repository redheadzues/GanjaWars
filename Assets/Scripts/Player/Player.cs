using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private float _minIntoxication = 0;
    private float _percent = 100;
    private float _currentSpeed;
    private float _currentIntoxication;
    private float _currentStrenght;
    private float _currentStamina;
    private float _abilityReducionMultiplier;
    private float _currentChanceDodge;
    private WaitForSeconds _waitingTime = new WaitForSeconds(0.2f);

    public float Score { get; private set; }
    public float SkillSpeed { get; private set; }
    public float SkillStrenght { get; private set; }
    public float SkillDodge { get; private set; }
    public float SkillStamina { get; private set; }
    public float ReduceMultiplier { get; private set; }
    public float ScorePerTime { get; private set; }
    public float DetoxicationPower { get; private set; }
    public float MaxIntoxication { get; private set; }
    public int  Money { get; private set; }

    public float CurrentMoveSpeed => _currentSpeed;
    public float CurrentStrenght => _currentStrenght;
    public Collider2D Target => _target;

    private void Start()
    {
        InitializeSkills();
        StartCoroutine(Detoxication());
        StartCoroutine(IncreaseScore());
    }

    private void Update()
    {
        CalculateCurrentAbility();
    }

    public void PickUpWeed(float weedPower)
    {
        _currentIntoxication = Mathf.Clamp(_currentIntoxication + weedPower, _minIntoxication, MaxIntoxication);
        IntoxicationChanged?.Invoke(_currentIntoxication, _percent);
        WeedTaked?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        _currentIntoxication -= damage;
        IntoxicationChanged?.Invoke(_currentIntoxication, _percent);   
        WeedLosed?.Invoke();
    }

    public void MeetPolice(int multiplier, int fine)
    {
        _currentIntoxication = _currentIntoxication / multiplier;
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

    public void IncreaseSkill(string name, float value)
    {
        switch(name)
        {
            case "Speed":
                SkillSpeed += value;
                break;
            case "Strenght":
                SkillStrenght += value;
                break;
            case "Stamina":
                SkillStamina += value;
                break;
            case "Dodge":
                SkillDodge += value;
                break;
            case "ReduceMultiplier":
                ReduceMultiplier += value;
                break;
            case "ScorePerTime":
                ScorePerTime += value;
                break;
            case "MaxIntoxication":
                MaxIntoxication += value;
                break;
        }
    }

    private void InitializeSkills()
    {
        SkillSpeed = 4;
        SkillStrenght = 3;
        SkillDodge = 1;
        SkillStamina = 3;
        ReduceMultiplier = 0.7f;
        ScorePerTime = 2;
        DetoxicationPower = 0.2f;
        MaxIntoxication = 100;
    }

    private void CalculateCurrentAbility()
    {
        _abilityReducionMultiplier = 1 - Mathf.Clamp((ReduceMultiplier * _currentIntoxication / MaxIntoxication), 0.0001f, ReduceMultiplier);

        _currentSpeed = SkillSpeed * _abilityReducionMultiplier ;
        _currentStrenght = SkillStrenght * _abilityReducionMultiplier;
        _currentChanceDodge = SkillDodge * _abilityReducionMultiplier;
    }

    private IEnumerator Detoxication()
    {
        while(true)
        {
            _currentIntoxication = Mathf.Clamp(_currentIntoxication - DetoxicationPower, _minIntoxication, MaxIntoxication);
            IntoxicationChanged?.Invoke(_currentIntoxication, _percent);

            yield return _waitingTime;  
        }
    }

    private IEnumerator IncreaseScore()
    {
        while(true)
        {
            Score += ScorePerTime * _currentIntoxication / MaxIntoxication;
            ScoreChanged?.Invoke(Score);

            yield return _waitingTime;
        }
    }
}
