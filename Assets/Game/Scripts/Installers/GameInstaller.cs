using Asteroids.Game.Core;
using Asteroids.Game.Management;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using Zenject;

namespace Asteroids.Game.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<UnityEngine.Object, GameEntity, GameEntity.Factory>()
                .FromFactory<PrefabFactory<GameEntity>>().WhenInjectedInto<GameEntitySpawnService>();

            Container.BindInterfacesAndSelfTo<SignalService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyWavesSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEntitySpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConfigCollectionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProfileService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle().WhenInjectedInto<GameContainer>();

            Container.Bind<IGameState>().To<GameReadyState>().AsSingle().WhenInjectedInto<GameContainer>();
            Container.Bind<IGameState>().To<GameLoadState>().AsSingle().WhenInjectedInto<GameContainer>();
            Container.Bind<IGameState>().To<GameRunningState>().AsSingle().WhenInjectedInto<GameContainer>();
            Container.Bind<IGameState>().To<GameOverState>().AsSingle().WhenInjectedInto<GameContainer>();
        }
    }
}