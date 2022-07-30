using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RocketStunDisplayer : MonoBehaviour
{
    [SerializeField] Rocket _rocket;
    [SerializeField] private TMP_Text _stunTime;

    private void OnEnable()
    {
        _rocket.StunTimeIncreased += ChangeStunTime;
    }
    private void OnDisable()
    {
        _rocket.StunTimeIncreased -= ChangeStunTime;
    }

    private void ChangeStunTime(int time)
    {
        _stunTime.text = time.ToString();
    }
}
