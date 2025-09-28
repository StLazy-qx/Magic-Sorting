using Zenject;

//скорее всего не понадобится
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Wallet>()
            .AsSingle()
            .NonLazy();
    }
}
