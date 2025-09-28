using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DifficultyState>().AsSingle().NonLazy();
        Container.Bind<Wallet>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameHandler>().AsSingle();
        Container.Bind<AudioSettingsData>().AsSingle();
    }
}
