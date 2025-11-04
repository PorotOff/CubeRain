using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _defaultPoolSize = 10;
    [SerializeField] private int _maxPoolSize = 20;
    [Header("Spawner settings")]
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;
    [SerializeField] private float _spawnPauseSeconds = 1.5f;

    private IObjectPool<Cube> _cubesPool;

    private void Awake()
        => _cubesPool = new ObjectPool<Cube>(OnPoolCreate, OnPoolGet, OnPoolRelease, OnPoolDestroy, true, _defaultPoolSize, _maxPoolSize);

    private void Start()
        => StartCoroutine(Spawn());

    private Cube OnPoolCreate()
        => Instantiate(_cubePrefab, _container);

    private void OnPoolGet(Cube cube)
        => cube.gameObject.SetActive(true);

    private void OnPoolRelease(Cube cube)
        => cube.gameObject.SetActive(false);

    private void OnPoolDestroy(Cube cube)
        => Destroy(gameObject);

    private IEnumerator Spawn()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_spawnPauseSeconds);

        while (enabled)
        {
            Cube cube = _cubesPool.Get();

            float randomXPosition = Random.Range(_minSpawnPosition.position.x, _maxSpawnPosition.position.x);
            float randomYPosition = Random.Range(_minSpawnPosition.position.y, _maxSpawnPosition.position.y);
            float randomZPosition = Random.Range(_minSpawnPosition.position.z, _maxSpawnPosition.position.z);
            Vector3 position = new Vector3(randomXPosition, randomYPosition, randomZPosition);

            cube.transform.position = position;

            cube.Destroyed += ReturnToPool;

            yield return wait;
        }
    }

    private void ReturnToPool(Cube cube)
    {
        cube.Destroyed -= ReturnToPool;
        _cubesPool.Release(cube);
    }
}