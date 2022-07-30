using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _maxMoney;
    [SerializeField] private int _minMoney;

    public int GetCount()
    {
        int money = Random.Range(_minMoney, _maxMoney);
        return money;
    }
}
