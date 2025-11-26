using System.Linq;
using UnityEngine;
using Assets.Source.Scripts.Colorize;
using Assets.Source.Scripts.GameDifficulty;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Vessels;
using System;

namespace Assets.Source.Scripts.Factory
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
            ValidateBuildRequirements();
            ClearList();

            DifficultySettings settings = DifficultyDatabase.
                GetSettings(DifficultyState.CurrentDifficulty);

            if (settings.vesselsCount <= 0)
                throw new InvalidOperationException("Vessels count must be > 0");

            for (int i = 0; i < settings.vesselsCount; i++)
            {
                Vessel vessel = Instantiate(Prefab);
                vessel.Filled += OnPutRemainingVessel;

                vessel.gameObject.SetActive(false);
                Add(vessel);
            }

            AssignColorsCount(CurrentSettings.colorsCount);
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
            int index = 0;

            foreach (var vessel in Objects)
            {
                if (index >= SpawnPoints.Length)
                    break;

                if (vessel.IsFilled == false && vessel.gameObject.activeSelf == false)
                {
                    vessel.transform.position = SpawnPoints[index].position;
                    vessel.gameObject.SetActive(true);

                    index++;
                }
            }
        }

        private void AssignColorsCount(int colorsCount)
        {
            if (colorsCount <= 0)
                throw new InvalidOperationException("colorsCount must be > 0");

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
            if (vessel == null)
                throw new ArgumentNullException(nameof(vessel));

            if (vessel.TryGetComponent(out ColorMarker colorMarker))
            {
                colorMarker.Initialize(color);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Vessel does not have required {nameof(ColorMarker)} component");
            }
        }

        private void ValidateBuildRequirements()
        {
            if (_gameFullingBehaviour == null)
                throw new ArgumentNullException(nameof(_gameFullingBehaviour));

            if (_distributerMagicCell == null)
                throw new ArgumentNullException(nameof(_distributerMagicCell));

            if (_colorRandomizer == null)
                throw new ArgumentNullException(nameof(_colorRandomizer));
        }
    }
}