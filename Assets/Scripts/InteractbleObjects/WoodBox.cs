using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBox : TakeableObject
{
    [SerializeField] private Money _money;

    private float _stunTime = 2;
    private int _strenght = 45;
    private int _currentStrenght = 45;
    private int _damage = 15;
    private float _minRandomValue = -0.5f;
    private float _maxRandomValue = 0.5f;

    public override void Throw(Vector3 targetPoint)
    {
        StartCoroutine(OnThrow(targetPoint, ToHarm));
    }

    private void ToHarm()
    {
        _currentStrenght -= _damage;

        if(_currentStrenght <= 0)
            Break();        
    }

    private void Break()
    {
        var randomPoint = transform.position + GetRandomPoint();
        Instantiate(_money, randomPoint, Quaternion.identity);
        ResetParametrs();
        gameObject.SetActive(false);
    }

    private void ResetParametrs()
    {
        _currentStrenght = _strenght;
        Target = null;
    }

    private Vector3 GetRandomPoint()
    {
        float randomPointX = Random.Range(_minRandomValue, _maxRandomValue);
        float randomPointY = Random.Range(_minRandomValue, _maxRandomValue);

        Vector3 point = new Vector3(randomPointX, randomPointY, 0);

        return point;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Target != null && collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            if(player.TryGetComponent(out PlayerCollisionHandler handler))
                handler.Stun(_stunTime);
            Break();
        }
    }
}
