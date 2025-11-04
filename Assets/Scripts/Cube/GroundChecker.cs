using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public event Action Grounded;

    private void OnCollisionEnter(Collision collision)
        => Grounded?.Invoke();
}