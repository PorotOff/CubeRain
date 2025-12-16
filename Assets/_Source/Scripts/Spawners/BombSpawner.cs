using UnityEngine;

public class BombSpawner : Spawner
{
    [Header("BombSpawner settings")]
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
        => _cubeSpawner.CubeReleasedAtPosition += OnCubeReleased;

    private void OnDisable()
        => _cubeSpawner.CubeReleasedAtPosition -= OnCubeReleased;

    private void OnCubeReleased(Vector3 position)
        => Spawn(position);
}