using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxIntoxication : Skill
{
    private float _maxValue = 140;
    private string _description = "Максимум интоксикации";
    private float _upgradeCost = 5;
    private float _upgradeValue = 5;

    public override void Increase(PlayerSkills playerSkills)
    {
        playerSkills.IncreaseSkill(this);
    }

    public override void SetValues(PlayerSkills skill)
    {
        Value = skill.MaxIntoxication;
        MaxValue = _maxValue;
        Description = _description;
        UpgradeCost = Value * _upgradeCost;
        UpgrageValue = _upgradeValue;
    }
}
