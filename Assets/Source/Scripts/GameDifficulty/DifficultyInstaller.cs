using UnityEngine;
using Zenject;

namespace GameDifficulty
{
    public class DifficultyInstaller : MonoInstaller
    {
        [SerializeField] private DifficultyDatabase _difficultyDatabase;

        public override void InstallBindings()
        {
            Container.Bind<DifficultyState>().AsSingle().NonLazy();

            if (_difficultyDatabase != null)
            {
                Container.BindInstance(_difficultyDatabase).WhenInjectedInto<LevelDifficultyViewer>();
            }
        }
    }
}