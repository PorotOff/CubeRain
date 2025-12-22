using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour, IPooledObject
{
    [Header("Destroying settings")]
    [SerializeField, Min(0)] private float _minDestroyingTimeSeconds;
    [SerializeField, Min(0)] private float _maxDestroyingTimeSeconds;

    private GroundChecker _groundChecker;
    private ColorChanger _colorChanger;

    public event Action<IPooledObject> Destroyed;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
        => _groundChecker.Grounded += OnGrounded;

    private void OnDisable()
        => _groundChecker.Grounded -= OnGrounded;

    private void OnGrounded()
    {
        _colorChanger.ChangeRandom();
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        float destroyingTime = UnityEngine.Random.Range(_minDestroyingTimeSeconds, _maxDestroyingTimeSeconds);
        yield return new WaitForSeconds(destroyingTime);
        Destroyed?.Invoke(this);
    }
}