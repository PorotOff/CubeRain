using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action<bool> OpenedStatistic;

    private bool _isStatisticOpened = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _isStatisticOpened = !_isStatisticOpened;
            OpenedStatistic?.Invoke(_isStatisticOpened);
        }
    }
}