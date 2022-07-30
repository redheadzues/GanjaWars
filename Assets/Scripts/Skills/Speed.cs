using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Skill
{
    private float _maxValue = 10;
    private string _description = "Скорость";
    private float _upgradeCost = 50;
    private float _upgradeValue = 1;

    public override void IncreaseSkill(Player player)
    {
        player.IncreaseSkill("Speed", _upgradeValue);
    }

    public override void SetValues(Player player)
    {
        Value = player.SkillSpeed;
        MaxValue = _maxValue;
        Description = _description;
        UpgradeCost = Value * _upgradeCost;
    }
}
