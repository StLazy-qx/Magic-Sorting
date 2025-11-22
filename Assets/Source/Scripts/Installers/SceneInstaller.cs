using Zenject;
using GameDifficulty;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelDifficultyViewer>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}
