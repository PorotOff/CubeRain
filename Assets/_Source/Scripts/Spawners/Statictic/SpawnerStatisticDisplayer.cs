using TMPro;
using UnityEngine;

public class SpawnerStatisticDisplayer<T, K> : MonoBehaviour where T : Spawner<K> where K : MonoBehaviour, IPooledObject
{
    [SerializeField] private Spawner<K> _spawner;
    [Header("Spawner texts")]
    [SerializeField] private TextMeshProUGUI _spawnerLabelText;
    [SerializeField] private TextMeshProUGUI _spawnerInstancesCountText;
    [SerializeField] private TextMeshProUGUI _spawnedEverCountText;
    [SerializeField] private TextMeshProUGUI _activeCountText;
    [Header("Spawner label")]
    [SerializeField] private string _spawnerLabel = "Название спавнера";

    private string _instancesCountLabel = "Количество созданных объектов";
    private string _spawnedEverCountLabel = "Количество заспавненных когда-либо объектов";
    private string _activeCountLabel = "Количество активных объектов";
    private string _separator = ": ";

    private void Start()
    {
        _spawnerLabelText.text = _spawnerLabel;
        UpdateStatistic();
    }

    private void OnEnable()
        => _spawner.DataChanged += UpdateStatistic;

    private void OnDisable()
        => _spawner.DataChanged -= UpdateStatistic;

    private void UpdateStatistic()
    {
        _spawnerInstancesCountText.text = $"{_instancesCountLabel}{_separator}{_spawner.InstancesCount}";
        _spawnedEverCountText.text = $"{_spawnedEverCountLabel}{_separator}{_spawner.SpawnedEverCount}";
        _activeCountText.text = $"{_activeCountLabel}{_separator}{_spawner.ActiveCount}";
    }
}