using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [Header("Collidind settings")]
    [SerializeField] private string _groundTag = "Ground";
    [Header("Destroying settings")]
    [SerializeField, Min(0)] private float _minDestroyingTimeSeconds;
    [SerializeField, Min(0)] private float _maxDestroyingTimeSeconds;

    private ColorChanger _colorChanger;

    private bool _isChecking = true;

    public event Action<Cube> Destroyed;

    private void Awake()
        => _colorChanger = GetComponent<ColorChanger>();

    private void OnEnable()
        => _isChecking = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _groundTag && _isChecking)
        {
            _colorChanger.RandomChange();
            StartCoroutine(DelayedDestroy());
            _isChecking = false;
        }
    }

    private IEnumerator DelayedDestroy()
    {
        float destroyingTime = UnityEngine.Random.Range(_minDestroyingTimeSeconds, _maxDestroyingTimeSeconds);
        yield return new WaitForSeconds(destroyingTime);
        Destroyed?.Invoke(this);
    }
}