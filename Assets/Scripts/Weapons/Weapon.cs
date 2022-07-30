using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Weapon _prefab;

    public int Price => _price;
    public string Label => _label;  
    public Sprite Icon => _icon;

    protected Collider2D Target;

    public void Use(Collider2D target, Transform shootPoint)
    {
        var weapon = Instantiate(_prefab, shootPoint.position, Quaternion.identity);
        weapon.Initialize(target);
    }

    public abstract void Initialize(Collider2D target);

}
