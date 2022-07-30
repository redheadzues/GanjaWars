using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private GameObject _container;
    [SerializeField] private WeaponView _template;
    [SerializeField] private Player _player;
    [SerializeField] private ArmoryPlayer _armory;

    private List<WeaponView> _views = new List<WeaponView>();

    private void Start()
    {
        for(int i = 0; i < _weapons.Count; i++)
        {
            AddWeapon(_weapons[i]);
        }
    }

    private void AddWeapon(Weapon weapon)
    {
        var view = Instantiate(_template, _container.transform);
        view.FillWeaponView(weapon);
        view.SellButtonClick += TrySellWeapon;
        _views.Add(view);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].SellButtonClick += TrySellWeapon;
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < _views.Count; i++)
        {
            _views[i].SellButtonClick -= TrySellWeapon;
        }
    }    

    private void TrySellWeapon(Weapon weapon)
    {
        if(_player.Money > weapon.Price)
        {
            _player.SpendMoney(weapon.Price);
            _armory.AddAmmo(weapon);
        }
    }
}
