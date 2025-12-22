using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TransparentizerModel))]
public class Bomb : MonoBehaviour, IPooledObject
{
    [SerializeField, Min(0)] private float _minExplosionTimeSeconds = 2f;
    [SerializeField, Min(0)] private float _maxExplosionTimeSeconds = 5f;

    [field: SerializeField, Min(0)] public float Scale { get; private set; } = 1f;
    [field: SerializeField, Min(0)] public float ExplosionRadius { get; private set; } = 10f;
    [field: SerializeField, Min(0)] public float ExplosionForce { get; private set; } = 10f;

    private TransparentizerModel _transparentizerModel;
    private ExploderModel _exploderModel;

    private float _explosionTimeSeconds;

    public event Action<IPooledObject> Destroyed;

    private void Awake()
    {
        _transparentizerModel = GetComponent<TransparentizerModel>();
        _exploderModel = new ExploderModel();
    }

    private void OnEnable()
    {
        _explosionTimeSeconds = UnityEngine.Random.Range(_minExplosionTimeSeconds, _maxExplosionTimeSeconds);
        _transparentizerModel.StartTransparentizing(_explosionTimeSeconds);
        StartCoroutine(ExplodeByTimer());
    }

    private IEnumerator ExplodeByTimer()
    {
        yield return new WaitForSecondsRealtime(_explosionTimeSeconds);
        _exploderModel.Explode(transform.position, ExplosionRadius, ExplosionForce);
        Destroyed?.Invoke(this);
    }
}