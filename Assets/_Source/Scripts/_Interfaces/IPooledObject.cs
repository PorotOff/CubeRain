using System;

public interface IPooledObject
{
    public event Action<IPooledObject> Destroyed;
}