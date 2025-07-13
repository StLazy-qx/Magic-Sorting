using System;
using System.Collections.Generic;
using UnityEngine;

public class VesselFactory : MonoBehaviour
{
    [SerializeField] private int count = 3;
    [SerializeField] private Vessel _prefab;
    [SerializeField] private Transform[] _points;

    private ColorRandomizer _colorRandomizer;
    private List<Vessel> _vessels;

    private void Awake()
    {
        _colorRandomizer = GetComponent<ColorRandomizer>();
    }

    private void Start()
    {
        Create();
    }

    private void Create()
    {
        _vessels = new List<Vessel>();

        Color[] colors = _colorRandomizer.CreateOriginalArrayColor(count);
        int spawnCount = Mathf.Min(count, colors.Length, _points.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Debug.Log(colors[i]);

            Vessel vessel = Instantiate(
                _prefab, 
                _points[i].position, 
                Quaternion.identity, 
                _points[i]);

            vessel.Init(colors[i]);
            _vessels.Add(vessel);
        }
    }
}
