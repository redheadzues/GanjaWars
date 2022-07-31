using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : Weapon
{
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    public event UnityAction<int> StunTimeIncreased;

    private float _proportion = 0.1f;
    private float _stunTime;
    private float _speed;
    private Vector3 _targetLastPosition;
    private Vector2 _direction;

    private void Update()
    {
        if(_targetLastPosition != Target.transform.position)
        {
            _targetLastPosition = Target.transform.position;
            _direction = _targetLastPosition - transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetLastPosition, _speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
    }

    public override void Initialize(Collider2D target)
    {
        Target = target;
        _targetLastPosition = Target.transform.position;
        _direction = _targetLastPosition - transform.position;
        StartCoroutine(IncreaseParametrs());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player) && collision == Target)
        {
            player.TakeDamage(_damage);
            if (player.TryGetComponent(out PlayerCollisionHandler handler))
                handler.Stun(_stunTime);
            Destroy(gameObject);
        }
    }

    private IEnumerator IncreaseParametrs()
    {
        var waitingTime = new WaitForSeconds(_proportion);

        for(int i = 0; i < _lifeTime/_proportion; i++)
        {
            _speed += _proportion;
            _damage += _proportion;
            _stunTime += _proportion;
            StunTimeIncreased?.Invoke((int)_stunTime);

            yield return waitingTime;
        }

        Destroy(gameObject);
    }
}
