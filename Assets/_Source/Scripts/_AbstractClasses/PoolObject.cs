using System;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public abstract event Action<PoolObject> Destroyed;
}