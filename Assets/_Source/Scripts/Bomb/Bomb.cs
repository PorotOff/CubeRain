using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transparentizer))]
public class Bomb : PoolObject
{
    [SerializeField, Min(0)] private float _minExplosionTimeSeconds = 2f;
    [SerializeField, Min(0)] private float _maxExplosionTimeSeconds = 5f;

    [field: SerializeField, Min(0)] public float Scale { get; private set; } = 1f;
    [field: SerializeField, Min(0)] public float ExplosionRadius { get; private set; } = 10f;
    [field: SerializeField, Min(0)] public float ExplosionForce { get; private set; } = 10f;

    private Transparentizer _transparentizer;

    private Exploder _exploder;

    public override event Action<PoolObject> Destroyed;

    public float ExplosionTimeSeconds => UnityEngine.Random.Range(_minExplosionTimeSeconds, _maxExplosionTimeSeconds);

    private void Awake()
    {
        _exploder = new Exploder();
        _transparentizer = GetComponent<Transparentizer>();
    }

    private void OnEnable()
    {
        _transparentizer.StartTransparentizing(ExplosionTimeSeconds);
        StartCoroutine(ExplodeByTimer());
    }

    private IEnumerator ExplodeByTimer()
    {
        yield return new WaitForSecondsRealtime(ExplosionTimeSeconds);
        _exploder.Explode(transform.position, ExplosionRadius, ExplosionForce);
        Destroyed?.Invoke(this);
    }
}