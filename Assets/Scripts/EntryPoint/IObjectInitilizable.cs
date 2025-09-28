using UnityEngine;

// подумать над названием
public interface IObjectInitilizable
{
    public bool IsInitialize { get; }

    public void Initilize();
}
