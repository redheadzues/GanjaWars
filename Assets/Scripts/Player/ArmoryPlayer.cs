using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmoryPlayer : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    public event UnityAction<Sprite, int> WeaponChanged;

    private List<int> _ammoAmount = new List<int>();
    private int _currentWeaponIndex = 0;
    private int _ammoCountOnBuy = 1;
    private Weapon _currentWeapon;

    private void Start()
    {
        for(int i = 0; i < _weapons.Count; i++)
        {
            _ammoAmount.Add(0);
        }

        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }
    
    public void NextWeapon()
    {
        if(_currentWeaponIndex == _weapons.Count -1)
            _currentWeaponIndex = 0;
        else
            _currentWeaponIndex++;

        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }

    public void UseWeapon(Collider2D target)
    {
        if (_ammoAmount[_currentWeaponIndex] > 0)
        {
            _currentWeapon.Use(target, transform);
            _ammoAmount[_currentWeaponIndex] -= 1;
            WeaponChanged?.Invoke(_currentWeapon.Icon, _ammoAmount[_currentWeaponIndex]);
        }
    }

    public void AddAmmo(Weapon weapon)
    {
        int index = _weapons.IndexOf(weapon);
        _ammoAmount[index] += _ammoCountOnBuy;
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        WeaponChanged?.Invoke(weapon.Icon, _ammoAmount[_currentWeaponIndex]);
    }
}
