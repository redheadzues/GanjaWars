using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    public event UnityAction<Weapon> SellButtonClick;

    private Weapon _weapon;

    public void FillWeaponView(Weapon weapon)
    {
        _weapon = weapon;

        _price.text += $" { weapon.Price.ToString()}";
        _label.text = weapon.Label;
        _icon.sprite = weapon.Icon;
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    private void OnSellButtonClick()
    {
        SellButtonClick?.Invoke(_weapon);
    }
}

