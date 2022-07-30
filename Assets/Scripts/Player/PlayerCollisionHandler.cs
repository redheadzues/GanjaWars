using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(PlayerControler))]
public class PlayerCollisionHandler : MonoBehaviour
{
    public event UnityAction<bool> isStunned;

    private Player _player;
    private PlayerControler _controler;

    private void Start()
    {
        _player = GetComponent<Player>();
        _controler = GetComponent<PlayerControler>();   
    }

    public void Stun(float time)
    {
        StartCoroutine(OnStun(_controler, time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ExplodeArea explodeArea))
            _player.TakeDamage(explodeArea.Damage);

        if (collision.TryGetComponent(out Marijuana marijuana))
        {
            _player.PickUpWeed(marijuana.CurrentPower);
            marijuana.gameObject.SetActive(false);
        }

        if(collision.TryGetComponent(out PolicePatroler patroler))
        {
            _player.MeetPolice(patroler.CalculateMultiplicator(), patroler.Fine);
            patroler.gameObject.SetActive(false);
        }

        if(collision.TryGetComponent(out Money money))
        {
            _player.AddMoney(money.GetCount());
            Destroy(money.gameObject);
        }
    }

    private IEnumerator OnStun(PlayerControler controler, float time)
    {
        var waitingTime = new WaitForSeconds(time);

        isStunned?.Invoke(true);
        controler.enabled = false;
        yield return waitingTime;
        isStunned?.Invoke(false);
        controler.enabled = true;
    }
}
