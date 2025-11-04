using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _defaultPoolSize = 10;
    [SerializeField] private int _maxPoolSize = 20;

    private IObjectPool<Cube> _cubesPool;

    private void Awake()
        => _cubesPool = new ObjectPool<Cube>(OnPoolCreate, OnPoolGet, OnPoolRelease, OnPoolDestroy, true, _defaultPoolSize, _maxPoolSize);

    private Cube OnPoolCreate()
        => Instantiate(_cubePrefab, _container);

    private void OnPoolGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.gameObject.AddComponent<GroundCollisionHandler>();
    }

    private void OnPoolRelease(Cube cube)
        => cube.gameObject.SetActive(false);

    private void OnPoolDestroy(Cube cube)
        => Destroy(cube);

    public Cube GetCube()
        => _cubesPool.Get();

    public void ReleaseCube(Cube cube)
        => _cubesPool.Release(cube);
}