using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectsPool
{
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private void Start()
    {
        Initialize(_templates);
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        var waitingTime = new WaitForSeconds(_secondsBetweenSpawn);

        while(true)
        {
            if(TryGetObject(out GameObject prefab))
            {
                int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

                SetPrefab(_spawnPoints[spawnPointIndex], prefab);
            }

            yield return waitingTime;
        }
    }

    private void SetPrefab(Transform spawnPoint, GameObject prefab)
    {
        if(prefab.TryGetComponent(out PolicePatroler patroler))
        {
            PathDefinition(patroler, spawnPoint);
        }

        prefab.SetActive(true);
        prefab.transform.position = spawnPoint.position;
    }

        private Transform[] _points;

    private void PathDefinition(PolicePatroler patroler, Transform spawnPoint)
    {
        int currentWayPoint = Random.Range(0, spawnPoint.childCount);
        
        _points = new Transform[spawnPoint.GetChild(currentWayPoint).childCount];

        for (int i = 0; i < spawnPoint.GetChild(currentWayPoint).childCount; i++)
        {

            _points[i] = spawnPoint.GetChild(currentWayPoint).GetChild(i);
        }

        patroler.Initialize(_points);
    }
}
