using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
    [Header("CubeSpawner settings")]
    [SerializeField] private float _spawnIntervalSeconds = 1.5f;
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;

    private Coroutine _coroutine;

    public event Action<Vector3> CubeReleasedAtPosition;

    private void OnEnable()
        => StartSpawn();

    private void OnDisable()
        => StopSpawn();

    private void StartSpawn()
    {
        StopSpawn();
        _coroutine = StartCoroutine(SpawnWithCertainFrequency());
    }

    private void StopSpawn()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnWithCertainFrequency()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_spawnIntervalSeconds);

        while (enabled)
        {
            float randomXPosition = UnityEngine.Random.Range(_minSpawnPosition.position.x, _maxSpawnPosition.position.x);
            float randomYPosition = UnityEngine.Random.Range(_minSpawnPosition.position.y, _maxSpawnPosition.position.y);
            float randomZPosition = UnityEngine.Random.Range(_minSpawnPosition.position.z, _maxSpawnPosition.position.z);
            Vector3 position = new Vector3(randomXPosition, randomYPosition, randomZPosition);
            
            Spawn(position);

            yield return wait;
        }
    }

    protected override void OnPoolObjectDestroyed(PoolObject poolObject)
    {
        base.OnPoolObjectDestroyed(poolObject);
        CubeReleasedAtPosition?.Invoke(poolObject.transform.position);
    }
}