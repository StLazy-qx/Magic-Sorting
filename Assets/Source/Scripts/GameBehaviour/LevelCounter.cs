using Assets.Source.Scripts.Extensions;
using System;
using UnityEngine;

namespace Assets.Source.Scripts.GameBehaviour
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField ]private FinalGameSession _finalGameSession;

        private int _currentRound = 1;

        public event Action<int> RoundChanged;

        private void Awake()
        {
            Guard.NotNull(_finalGameSession, nameof(_finalGameSession));
        }

        private void OnEnable()
        {
            _finalGameSession.RoundEnded += OnRoundChange;
        }

        private void OnDisable()
        {
            _finalGameSession.RoundEnded -= OnRoundChange;
        }

        private void OnRoundChange()
        {
            _currentRound++;

            RoundChanged?.Invoke(_currentRound);
        }
    }
}
