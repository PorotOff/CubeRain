using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private bool _isStatisticOpened = false;

    public event Action<bool> OpenedStatistic;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _isStatisticOpened = !_isStatisticOpened;
            OpenedStatistic?.Invoke(_isStatisticOpened);
        }
    }
}