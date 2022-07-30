using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Mine : Weapon
{
    [SerializeField] private float _secondsToFade;
    [SerializeField] private float _stepsFade;
    [SerializeField] private GameObject _explodeArea;

    private float _delayForExplode = 0.2f;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _selfCollider;
    private float _maxAlphaValue = 1;
    private float _timeToActivating = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OnExplode());
    }

    private IEnumerator OnExplode()
    {
        var delay = new WaitForSeconds(_delayForExplode);

        while (true)
        {
            _explodeArea.SetActive(true);
            yield return delay;
            Destroy(gameObject);
        }
    }

    private IEnumerator Fading()
    {
        var waitingTime = new WaitForSeconds(_secondsToFade/_stepsFade);
        var color = _spriteRenderer.color;

        for(int i = 0; i < _stepsFade; i++)
        {
            color.a = _maxAlphaValue - (_maxAlphaValue / _stepsFade * i);
            _spriteRenderer.color = color;

            yield return waitingTime;
        }
    }

    private IEnumerator Activating()
    {
        var waitingTime = new WaitForSeconds(_timeToActivating);

        yield return waitingTime;
        _selfCollider.enabled = true;
    }

    public override void Initialize(Collider2D target)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _selfCollider = GetComponent<Collider2D>();
        StartCoroutine(Activating());
        StartCoroutine(Fading());
    }
}
