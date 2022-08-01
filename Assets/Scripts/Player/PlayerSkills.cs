using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkills : MonoBehaviour
{
    private float _currentSpeed;
    private float _currentStrenght;
    private float _currentStamina;
    private float _abilityReducionMultiplier;
    private float _currentChanceDodge;
    private Player _player;

    public float Speed { get; private set; }
    public float Strenght { get; private set; }
    public float Dodge { get; private set; }
    public float Stamina { get; private set; }
    public float ReduceMultiplier { get; private set; }
    public float ScorePerTime { get; private set; }
    public float MaxIntoxication { get; private set; }

    public float CurrentMoveSpeed => _currentSpeed;
    public float CurrentStrenght => _currentStrenght;

    private void Start()
    {
        _player = GetComponent<Player>();
        InitializeSkills();
    }

    private void Update()
    {
        CalculateCurrentAbility();
    }

    private void InitializeSkills()
    {
        Speed = 4;
        Strenght = 3;
        Dodge = 1;
        Stamina = 3;
        ReduceMultiplier = 0.7f;
        ScorePerTime = 2;
        MaxIntoxication = 100;
    }

    private void CalculateCurrentAbility()
    {
        _abilityReducionMultiplier = 1 - Mathf.Clamp((ReduceMultiplier * _player.CurrentIntoxication / MaxIntoxication), 0.0001f, ReduceMultiplier);

        _currentSpeed = Speed * _abilityReducionMultiplier;
        _currentStrenght = Strenght * _abilityReducionMultiplier;
        _currentChanceDodge = Dodge * _abilityReducionMultiplier;
    }

    public void IncreaseSkill(Speed speed)
    {
        Speed += speed.UpgrageValue;
    }

    public void IncreaseSkill(Strenght strenght)
    {
        Strenght += strenght.UpgrageValue;
    }
    public void IncreaseSkill(Dodge dodge)
    {
        Dodge += dodge.UpgrageValue;
    }
    public void IncreaseSkill(Stamina stamina)
    {
        Stamina += stamina.UpgrageValue;
    }

    public void IncreaseSkill(ReduceMultiplier reduce)
    {
        ReduceMultiplier += reduce.UpgrageValue;
    }
    public void IncreaseSkill(ScorePerTime scorePerTime)
    {
        ScorePerTime += scorePerTime.UpgrageValue;
    }
    public void IncreaseSkill(MaxIntoxication maxIntoxication)
    {
        MaxIntoxication += maxIntoxication.UpgrageValue;
    }
}
