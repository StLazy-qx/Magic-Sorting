using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T> where T : MonoBehaviour
{
    IReadOnlyList<T> Instances { get; }

    void ResetFactory(DifficultyLevel level);
    void Build();
    void Clear();
}
