using System.Linq;
using UnityEngine;

public class VesselFactory : Factory<Vessel>
{
    [SerializeField] private VesselsFullingBehaviour _gameFullingBehaviour;
    [SerializeField] private MagicCellRouter _distributerMagicCell;
    [SerializeField] private ColumnColorDistributor _buildMagicColumn;
    [SerializeField] private ColorRandomizer _colorRandomizer;

    public bool IsReady { get; private set; }

    protected override void OnDestroy()
    {
        foreach (var vesell in Objects)
        {
            if (vesell != null)
                vesell.Filled -= OnReplaceVessel;
        }

        base.OnDestroy();
    }

    protected override void BuildObjects()
    {
        if (Prefab == null)
        {
            Debug.LogError($"{name}: Prefab не назначен в инспекторе!");

            return;
        }

        if (SpawnPoints == null || SpawnPoints.Length == 0)
        {
            Debug.LogWarning($"{name}: " +
                $"SpawnPoints пустые — сосуды будут созданы, но не заспавнены.");
        }

        ClearList();

        DifficultySettings difficultyState = DifficultyDatabase.
            GetSettings(DifficultyState.CurrentDifficulty);

        for (int i = 0; i < difficultyState.vesselsCount; i++)
        {
            Vessel vessel = Instantiate(Prefab);
            vessel.Filled += OnReplaceVessel;
            vessel.gameObject.SetActive(false);

            Add(vessel);
        }

        AssignColorsToVessels(CurrentSettings.colorsCount);
        //_distributerMagicCell.Initialize(Objects);
        //_buildMagicColumn.Initialize(Objects);
        //_gameFullingBehaviour.Init(Objects);
        SpawnVessels();

        IsReady = true;
    }

    private void OnReplaceVessel(Vector3 position)
    {
        Vessel newVessel = Objects.FirstOrDefault(
            vessel => vessel.IsFilled == false && vessel.gameObject.activeSelf == false);

        if (newVessel == null)
            return;

        newVessel.transform.position = position;

        newVessel.gameObject.SetActive(true);
    }

    private void SpawnVessels()
    {
        if (SpawnPoints == null || SpawnPoints.Length == 0)
            return;

        int index = 0;
        foreach (var vessel in Objects)
        {
            if (index >= SpawnPoints.Length)
                break;

            if (!vessel.IsFilled && !vessel.gameObject.activeSelf)
            {
                vessel.transform.position = SpawnPoints[index].position;
                vessel.gameObject.SetActive(true);
                index++;
            }
        }
    }

    private void AssignColorsToVessels(int countColors)
    {
        if (Objects.Count == 0)
            return;

        if (Objects.Count > countColors)
        {
            Color[] pointColors = _colorRandomizer.
                CrateArrayColors(Mathf.Min(countColors, SpawnPoints.Length));

            for (int i = 0; i < Mathf.Min(SpawnPoints.Length, Objects.Count); i++)
            {
                Color colorToAssign = i < pointColors.Length
                    ? pointColors[i]
                    : _colorRandomizer.GenerateRandomColor();
                Objects[i].GetComponent<ColorMarker>().Init(colorToAssign);
            }

            for (int i = SpawnPoints.Length; i < Objects.Count; i++)
            {
                Objects[i].GetComponent<ColorMarker>()
                    .Init(_colorRandomizer.GenerateRandomColor());
            }
        }
        else
        {
            Color[] colors = _colorRandomizer.CrateArrayColors(countColors);

            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].GetComponent<ColorMarker>()
                    .Init(colors[i % colors.Length]);
            }
        }
    }
}
