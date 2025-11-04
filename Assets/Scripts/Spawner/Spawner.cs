using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubesPool;
    [SerializeField] private Transform _minSpawnPosition;
    [SerializeField] private Transform _maxSpawnPosition;
    [SerializeField] private float _spawnPause = 1.5f;

    private void Start()
        => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_spawnPause);

        while (gameObject.activeSelf)
        {
            Cube cube = _cubesPool.GetCube();

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
        _cubesPool.ReleaseCube(cube);
    }
}