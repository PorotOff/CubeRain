using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minDestroyingTimeSeconds;
    [SerializeField, Min(0)] private float _maxDestroyingTimeSeconds;

    public event Action<Cube> Destroyed;

    public void StartDelayedDestroy()
        => StartCoroutine(DelayedDestroy());

    private IEnumerator DelayedDestroy()
    {
        float destroyingTime = UnityEngine.Random.Range(_minDestroyingTimeSeconds, _maxDestroyingTimeSeconds);
        yield return new WaitForSeconds(destroyingTime);
        Destroyed?.Invoke(this);
    }
}