using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TakeableObject : MonoBehaviour
{
    protected Vector3 StartPosition;
    protected Collider2D Target;
    protected Transform _parent;

    private float _timeTowait = 0.1f;

    public Collider2D TakeableObjectTarget => Target;
    private void Start()
    {
        StartPosition = transform.position;
    }

    public abstract void Throw(Vector3 targetPoint);

    public void SetTarget(Collider2D target)
    {
        Target = target;
    }

    protected IEnumerator OnThrow(Vector3 targetPoint, UnityAction arrived = null)
    {
        var waitingTime = new WaitForSeconds(_timeTowait);

        while (transform.position != targetPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, 10 * Time.deltaTime);
            yield return waitingTime;
        }

        Target = null;

        if (StartPosition != targetPoint)
        {
            arrived?.Invoke();
        }
    }
}
