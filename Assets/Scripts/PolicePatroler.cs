using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePatroler : MonoBehaviour
{
    [SerializeField] private int _fine;
    [SerializeField] private float _speed;
    [SerializeField] private int _minMeetPlayerMiltiplicator;
    [SerializeField] private int _maxMeetPlayerMiltiplicator;
    [SerializeField] private GameObject _money;

    public int Fine => _fine;

    private Transform[] _wayPoints;
    private int _currentPoint;    

    public void Initialize(Transform[] wayPoints)
    {
        _wayPoints = wayPoints;
        _currentPoint = 0;
    }

    public int CalculateMultiplicator()
    {
        int multiplicator = Random.Range(_minMeetPlayerMiltiplicator, _maxMeetPlayerMiltiplicator);

        return multiplicator;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Transform target = _wayPoints[_currentPoint];
        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position && _currentPoint < _wayPoints.Length)
            _currentPoint++;

        if (transform.position == _wayPoints[_wayPoints.Length - 1].position)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ExplodeArea explodeArea))
        {
            Instantiate(_money, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
