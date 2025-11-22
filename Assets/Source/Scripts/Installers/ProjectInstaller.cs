using Zenject;
using GameDifficulty;
using PlayerCore;
using Sound;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DifficultyState>().AsSingle().NonLazy();
        Container.Bind<Wallet>().AsSingle();
        Container.Bind<AudioSettingsData>().AsSingle();
    }
}
