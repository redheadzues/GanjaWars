using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ArmoryPlayer _armory;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _ammoAmount;
    [SerializeField] private Image _weaponIcon;

    private string _baseText;

    private void Start()
    {
        _score.text = "0";
        _money.text = "0";
        _baseText = _ammoAmount.text;
    }

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
        _player.MoneyChanged += OnMoneyChanged;
        _armory.WeaponChanged += OnWeaponChanged;

    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
        _player.MoneyChanged -= OnMoneyChanged;
        _armory.WeaponChanged -= OnWeaponChanged;
    }
    private void OnWeaponChanged(Sprite icon, int amount)
    {
        _ammoAmount.text = $"{_baseText} {amount.ToString()}";
        _weaponIcon.sprite = icon;

    }

    private void OnMoneyChanged(int money)
    {
        _money.text = money.ToString();
    }

    private void OnScoreChanged(float score)
    {
        _score.text = ((int)score).ToString();
    }
}
