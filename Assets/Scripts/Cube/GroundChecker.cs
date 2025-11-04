using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool _isChecking = true;

    public event Action Grounded;

    private void OnEnable()
        => _isChecking = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isChecking && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            Grounded?.Invoke();
            _isChecking = false;
        }
    }
}