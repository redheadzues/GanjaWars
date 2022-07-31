using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Skill
{
    private float _maxValue = 10;
    private string _description = "Скорость";
    private float _upgradeCost = 50;
    private float _upgradeValue = 1;

    public override void Increase(PlayerSkills playerSkills)
    {
        playerSkills.IncreaseSkill(this, _upgradeValue);
    }

    public override void SetValues(PlayerSkills skill)
    {
        Value = skill.Speed;
        MaxValue = _maxValue;
        Description = _description;
        UpgradeCost = Value * _upgradeCost;
        UpgrageValue = _upgradeValue;
    }
}
