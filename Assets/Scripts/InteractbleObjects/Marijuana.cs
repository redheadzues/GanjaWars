using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marijuana : MonoBehaviour
{
    [SerializeField] private float _maxPower;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _increasePowerStep;

    public float CurrentPower { get; private set;}
    private Vector3 _increaseSizeStep = new Vector3(0.1f, 0.1f, 0);
    private float growStep = 0.2f;
    private Vector3 _startScale;
    private Coroutine _coroutine;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    private void OnEnable()
    {
        ResetParametrs();

        if (_coroutine != null)
            StopCoroutine(Grow());

        _coroutine =  StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float currentLifeTime = 0;
        var waitingTime = new WaitForSeconds(growStep);

        while(currentLifeTime < _lifeTime)
        {
            CurrentPower = Mathf.Clamp(CurrentPower + _increasePowerStep, 0, _maxPower);
            transform.localScale += _increaseSizeStep;
            currentLifeTime += growStep;

            yield return waitingTime;
        }

        gameObject.SetActive(false);
    }

    private void ResetParametrs()
    {
        transform.localScale = _startScale;
        CurrentPower = 0;
    }
}
