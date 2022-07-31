using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : Skill
{
    private float _maxValue = 10;
    private string _description = "Уворот";
    private float _upgradeCost = 50;
    private float _upgradeValue = 1;

    public override void IncreaseSkill(PlayerSkills skill)
    {
        skill.IncreaseSkill(dodge:_upgradeValue);
    }

    public override void SetValues(PlayerSkills skill)
    {
        Value = skill.Dodge;
        MaxValue = _maxValue;
        Description = _description;
        UpgradeCost = Value * _upgradeCost;
    }
}
