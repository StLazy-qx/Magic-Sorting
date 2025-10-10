using UnityEngine;

public interface IObjectInitilizable
{
    public bool IsInitialized { get; }

    public void Initilize();
}
