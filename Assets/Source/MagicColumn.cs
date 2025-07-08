using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicColumn : MonoBehaviour
{
    [SerializeField] private int _countCells;
    [SerializeField] private MagicCell _cell;

    private List<MagicCell> _cells;

    private void Awake()
    {
        _cells = new();
    }

    void Start()
    {
        _cells = Spawn(_countCells, transform.position, _cell);
    }

    public List<MagicCell> Spawn(int count, Vector3 basePosition, MagicCell prefab)
    {
        float currentY = basePosition.y;
        float prefabHeight = GetPrefabHeight(prefab);

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(basePosition.x,
                currentY, basePosition.z);
            MagicCell cell = Instantiate(prefab, spawnPosition, Quaternion.identity);

            if (cell != null)
            {
                _cells.Add(cell);
                currentY += prefabHeight;
            }
        }

        return _cells;
    }

    private float GetPrefabHeight(MagicCell prefab)
    {
        Renderer renderer = prefab.GetComponentInChildren<Renderer>();

        return renderer.bounds.size.y + 0.05f;
    }
}
