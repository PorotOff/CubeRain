using System.Collections.Generic;
using UnityEngine;

public class SpawnerStaticticDisplayer : MonoBehaviour
{
    [SerializeField] private Statistic _statistic;
    [SerializeField] private List<Spawner> _spawners;

    private void Start()
    {
        foreach (var spawner in _spawners)
        {
            _statistic.AddValue(spawner.Name, spawner.InstancesCountStatisticLabel, spawner.InstancesCount.ToString());
            _statistic.AddValue(spawner.Name, spawner.SpawnedEverCountStatisticLabel, spawner.SpawnedEverCount.ToString());
            _statistic.AddValue(spawner.Name, spawner.ActiveCountCountStatisticLabel, spawner.ActiveCount.ToString());
        }
    }

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
            spawner.DataChanged += OnSpawnerDataChanged;
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
            spawner.DataChanged -= OnSpawnerDataChanged;
    }

    private void OnSpawnerDataChanged(string spawnerName, string label, string value)
        => _statistic.UpdateValue(spawnerName, label, value);
}