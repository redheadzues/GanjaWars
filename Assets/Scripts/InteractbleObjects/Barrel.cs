using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : TakeableObject
{
    [SerializeField] private GameObject _explodeArea;

    private float _delayForExplode = 0.2f;

    private void OnEnable()
    {
        _explodeArea.SetActive(false);
    }

    public override void Throw(Vector3 targetPoint)
    {
        StartCoroutine(OnThrow(targetPoint, Explode));
    }

    public void Explode()
    {
        StartCoroutine(OnExplode());
    }

    private IEnumerator OnExplode()
    {
        var delay = new WaitForSeconds(_delayForExplode);

        while (gameObject.activeSelf == true)
        {
            _explodeArea.SetActive(true);
            yield return delay;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PolicePatroler patroller))      
            Explode();
    }
}
