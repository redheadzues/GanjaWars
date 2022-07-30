using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(PlayerCollisionHandler))]
public class PlayerEffector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _takeMoney;
    [SerializeField] private ParticleSystem _loseMoney;
    [SerializeField] private ParticleSystem _takeWeed;
    [SerializeField] private ParticleSystem _loseWeed;
    [SerializeField] private ParticleSystem _stunned;

    private Player _player;
    private PlayerCollisionHandler _collisionHandler;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        _stunned.Stop();
    }

    private void OnEnable()
    {
        _player.MoneyTaked += TakeMoney;
        _player.WeedTaked += TakeWeed;
        _player.MoneyLosed += LoseMoney;
        _player.WeedLosed += LoseWeed;
        _collisionHandler.isStunned += OnStun;
    }

    private void OnDisable()
    {
        _player.MoneyTaked -= TakeMoney;
        _player.WeedTaked -= TakeWeed;
        _player.MoneyLosed -= LoseMoney;
        _player.WeedLosed -= LoseWeed;
        _collisionHandler.isStunned -= OnStun;
    }

    private void TakeMoney()
    {
        _takeMoney.Play();
    }

    private void LoseMoney()
    {
        _loseMoney.Play();
    }

    private void TakeWeed()
    {
        _takeWeed.Play();
    }

    private void LoseWeed()
    {
        _loseWeed.Play();
    }

    private void OnStun(bool isStunned)
    {
        if (isStunned)
            _stunned.Play();
        else
            _stunned.Stop();
    }
}
