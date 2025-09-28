using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Factory<T> : MonoBehaviour where T : MonoBehaviour // поменять на интефейс 
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform[] SpawnPoints;
    [SerializeField] protected DifficultyDatabase DifficultyDatabase;

    protected DifficultyState DifficultyState;
    protected DifficultySettings CurrentSettings;
    private List<T> _objects = new List<T>();

    public event Action<IReadOnlyList<T>> ListObjectsChanged;

    public IReadOnlyList<T> Objects => _objects;

    private void Start()
    {
        BuildObjects();
    }

    protected virtual void OnDestroy()
    {
        if(DifficultyState != null)
            DifficultyState.DifficultyChanged -= OnDifficultyChanged;
    }

    [Inject]
    public void Construct(DifficultyState difficultyState)
    {
        DifficultyState = difficultyState;

        if (DifficultyDatabase != null)
        {
            CurrentSettings = DifficultyDatabase.
                GetSettings(DifficultyState.CurrentDifficulty);
        }

        DifficultyState.DifficultyChanged += OnDifficultyChanged;

        //BuildObjects();
    }

    public virtual void ResetFactory(DifficultyLevel level)
    {
        ClearList();

        if (DifficultyState != null)
            DifficultyState.SetDifficulty(level);

        if (DifficultyDatabase != null)
            CurrentSettings = DifficultyDatabase.GetSettings(level);

        BuildObjects();
    }

    public virtual IReadOnlyList<T> GetListObjects()
    {
        return Objects;
    }

    public void NotifyObjectsChanged()
    {
        ListObjectsChanged?.Invoke(Objects);
    }

    protected abstract void BuildObjects();

    protected virtual void OnDifficultyChanged(DifficultyLevel level)
    {
        ResetFactory(level);
    }

    protected void Add(T @object)
    {
        if (@object != null)
            _objects.Add(@object);
    }

    protected virtual void ClearList()
    {
        foreach (var @object in Objects)
        {
            if (@object != null)
                Destroy(@object.gameObject);
        }

        _objects.Clear();
    }
}
