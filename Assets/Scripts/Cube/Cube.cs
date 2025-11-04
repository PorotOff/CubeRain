using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minDestroyingTime;
    [SerializeField, Min(0)] private float _maxDestroyingTime;

    public event Action<Cube> Destroyed;

    public void StartDelayedDestroy()
        => StartCoroutine(DelayedDestroy());

    private IEnumerator DelayedDestroy()
    {
        float destroyingTime = UnityEngine.Random.Range(_minDestroyingTime, _maxDestroyingTime);
        yield return new WaitForSeconds(destroyingTime);
        Destroyed?.Invoke(this);
    }
}