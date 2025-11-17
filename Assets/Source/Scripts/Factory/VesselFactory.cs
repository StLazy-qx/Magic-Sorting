using System.Linq;
using UnityEngine;
using Colorize;
using GameDifficulty;
using MagicCells;
using Vessels;

namespace FactoryCore
{
    public class VesselFactory : Factory<Vessel>
    {
        [SerializeField] private VesselStateTracker _gameFullingBehaviour;
        [SerializeField] private MagicCellRouter _distributerMagicCell;
        [SerializeField] private ColorRandomizer _colorRandomizer;

        public bool IsReady { get; private set; }

        protected override void OnDestroy()
        {
            foreach (var vesell in Objects)
            {
                if (vesell != null)
                    vesell.Filled -= OnPutRemainingVessel;
            }

            base.OnDestroy();
        }

        protected override void BuildObjects()
        {
            if (Prefab == null)
                return;

            ClearList();

            DifficultySettings difficultyState = DifficultyDatabase.
                GetSettings(DifficultyState.CurrentDifficulty);

            for (int i = 0; i < difficultyState.vesselsCount; i++)
            {
                Vessel vessel = Instantiate(Prefab);
                vessel.Filled += OnPutRemainingVessel;

                vessel.gameObject.SetActive(false);
                Add(vessel);
            }

            AssignColorsToVessels(CurrentSettings.colorsCount);
            _gameFullingBehaviour.SetVesselsList(Objects);
            ActivateVessels();

            IsReady = true;
        }

        private void OnPutRemainingVessel(Vector3 position)
        {
            Vessel remainingVessel = Objects.FirstOrDefault(
                vessel => vessel.IsFilled == false && vessel.gameObject.activeSelf == false);

            if (remainingVessel == null)
                return;

            remainingVessel.transform.position = position;

            remainingVessel.gameObject.SetActive(true);
        }

        private void ActivateVessels()
        {
            if (SpawnPoints == null || SpawnPoints.Length == 0)
                return;

            int index = 0;

            foreach (var vessel in Objects)
            {
                if (index >= SpawnPoints.Length)
                    break;

                if (vessel.IsFilled == false && vessel.gameObject.activeSelf == false)
                {
                    vessel.transform.position = SpawnPoints[index].position;
                    index++;

                    vessel.gameObject.SetActive(true);
                }
            }
        }

        private void AssignColorsToVessels(int colorsCount)
        {
            if (Objects.Count == 0)
                return;

            int realColorCount = Mathf.Min(colorsCount, Objects.Count);
            Color[] palette = _colorRandomizer.CrateArrayColors(realColorCount);

            for (int i = 0; i < Objects.Count; i++)
            {
                Color color = palette[i % palette.Length];

                AssignColor(Objects[i], color);
            }
        }

        private void AssignColor(Vessel vessel, Color color)
        {
            vessel.GetComponent<ColorMarker>().Initialize(color);
        }
    }
}