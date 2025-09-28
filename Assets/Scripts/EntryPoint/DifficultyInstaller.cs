using UnityEngine;
using Zenject;

public class DifficultyInstaller : MonoInstaller
{
    [SerializeField] private DifficultyDatabase _difficultyDatabase;

    public override void InstallBindings()
    {
        Container.Bind<DifficultyState>().AsSingle().NonLazy();

        if (_difficultyDatabase != null)
        {
            Container.BindInstance(_difficultyDatabase).WhenInjectedInto<LevelDifficultySetterUI>();
        }
    }
}
