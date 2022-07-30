using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public float Value { get; protected set; }
    public float MaxValue { get; protected set; }
    public string Description { get; protected set; }
    public float UpgradeCost { get; protected set; }

    public abstract void SetValues(Player player);
    public abstract void IncreaseSkill(Player player);
}
