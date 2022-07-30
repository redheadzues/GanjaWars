using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceMultiplier : Skill
{
    private float _minValue = 0.3f;
    private string _description = "Тормознутость";
    private float _upgradeCost = 5000;
    private float _upgradeValue = -0.1f;

    public override void IncreaseSkill(Player player)
    {
        player.IncreaseSkill("ReduceMultiplier", _upgradeValue);
    }

    public override void SetValues(Player player)
    {
        Value = player.ReduceMultiplier;
        MaxValue = _minValue;
        Description = _description;
        UpgradeCost = (1-Value) * _upgradeCost;
    }
}
