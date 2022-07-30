using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Wrench : TakeableObject
{
    private float _damage = 15;
    private float _stunTime = 5;
    private float _deactiveTime = 15;
    private Color _startColor;
    private Coroutine _coroutine;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }
    public override void Throw(Vector3 targetPoint)
    {
        StartCoroutine(OnThrow(targetPoint));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Target != null && collision.TryGetComponent(out Player player) == Target)
        {
            player.TakeDamage(_damage);
            if (player.TryGetComponent(out PlayerCollisionHandler handler))
                handler.Stun(_stunTime);
            Target = null;
            TemproraryDeactive();
        }
    }

    private void TemproraryDeactive()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OnTemproraryDeactive());
    }

    private IEnumerator OnTemproraryDeactive()
    {
        var waitingTime = new WaitForSeconds(_deactiveTime);

        _boxCollider.enabled = false;
        _spriteRenderer.color = Color.green;
        yield return waitingTime;
        _spriteRenderer.color = _startColor;
        _boxCollider.enabled = true;
    }
}
