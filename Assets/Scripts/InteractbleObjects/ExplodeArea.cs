using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private ParticleSystem _particle;

    public float Damage => _damage;

    private void OnEnable()
    {
        _particle.Play();
    }
}
