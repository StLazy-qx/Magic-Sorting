using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelDifficultySetterUI>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}
