using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePerTime : Skill
{
    private float _maxValue = 5;
    private string _description = "Добавление очков";
    private float _upgradeCost = 250;
    private float _upgradeValue = 0.5f;

    public override void Increase(PlayerSkills playerSkills)
    {
        playerSkills.IncreaseSkill(this, _upgradeValue);
    }

    public override void SetValues(PlayerSkills skill)
    {
        Value = skill.ScorePerTime;
        MaxValue = _maxValue;
        Description = _description;
        UpgradeCost = Value * _upgradeCost;
    }
}
