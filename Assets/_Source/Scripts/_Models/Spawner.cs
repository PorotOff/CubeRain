using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [Header("Spawner settings")]
    [SerializeField] private PoolObject _prefab;
    [SerializeField] private Transform _container;

    [field: Header("Spawner labels")]
    [field: SerializeField] public string Name { get; private set; } = "Название спавнера";
    [field: SerializeField] public string InstancesCountStatisticLabel { get; private set; } = "Количество созданных объектов";
    [field: SerializeField] public string SpawnedEverCountStatisticLabel { get; private set; } = "Количество заспавненных когда-либо объектов";
    [field: SerializeField] public string ActiveCountCountStatisticLabel { get; private set; } = "Количество активных объектов";

    private IObjectPool<PoolObject> _pool;

    public event Action<string, string, string> DataChanged;

    public int InstancesCount { get; private set; }
    public int SpawnedEverCount { get; private set; }
    public int ActiveCount { get; private set; }

    private void Awake()
        => _pool = new ObjectPool<PoolObject>(OnPoolCreate, OnPoolGet, OnPoolRelease, OnPoolDestroy);

    protected PoolObject Spawn(Vector3 position)
    {
        PoolObject spawnable = Spawn();
        spawnable.transform.position = position;

        return spawnable;
    }

    protected PoolObject Spawn()
    {
        PoolObject spawnable = _pool.Get();
        spawnable.Destroyed += OnPoolObjectDestroyed;

        SpawnedEverCount++;
        DataChanged?.Invoke(Name, SpawnedEverCountStatisticLabel, SpawnedEverCount.ToString());

        return spawnable;
    }

    protected virtual void OnPoolObjectDestroyed(PoolObject poolObject)
    {
        poolObject.Destroyed -= OnPoolObjectDestroyed;
        _pool.Release(poolObject);
    }

    private PoolObject OnPoolCreate()
    {
        PoolObject spawnable = Instantiate(_prefab, _container);
        InstancesCount++;

        DataChanged?.Invoke(Name, InstancesCountStatisticLabel, InstancesCount.ToString());

        return spawnable;
    }

    private void OnPoolGet(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(true);
        ActiveCount++;

        DataChanged?.Invoke(Name, ActiveCountCountStatisticLabel, ActiveCount.ToString());
    }

    private void OnPoolRelease(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        ActiveCount--;
        
        DataChanged?.Invoke(Name, ActiveCountCountStatisticLabel, ActiveCount.ToString());
    }

    private void OnPoolDestroy(PoolObject poolObject)
        => Destroy(poolObject.gameObject);
}