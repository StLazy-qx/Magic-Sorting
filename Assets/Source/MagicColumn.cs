using System.Collections.Generic;
using UnityEngine;

public class MagicColumn : MonoBehaviour
{
    [SerializeField] private int _countCells;
    [SerializeField] private MagicCell _cell;

    private Stack<MagicCell> _cellsStack;
    private float _prefabHeight;
    private float _distanceBetweenCells = 0.05f;


    private void Awake()
    {
        _cellsStack = new();
        _prefabHeight = GetPrefabHeight();
    }

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        float currentY = transform.position.y;

        for (int i = 0; i < _countCells; i++)
        {
            MagicCell cell = Instantiate(_cell, transform);

            if (cell != null)
            {
                cell.transform.localPosition = new Vector3(0, currentY, 0);

                _cellsStack.Push(_cell);

                currentY += _prefabHeight;
            }
        }
    }

    private float GetPrefabHeight()
    {
        Renderer renderer = _cell.GetComponentInChildren<Renderer>();

        return renderer.bounds.size.y + _distanceBetweenCells;
    }
}
