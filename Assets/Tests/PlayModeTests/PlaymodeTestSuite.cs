using Asteroids.Game.Config;
using Asteroids.Game.Core;
using Asteroids.Game.Management;
using Asteroids.Game.Runtime;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;
using Zenject;

public class PlaymodeTestSuite : ZenjectIntegrationTestFixture
{
    [UnityTest]
    public IEnumerator UT_PlayModeLoadAddressableTest()
    {
        PreInstall();
        PostInstall();

        var operation = Addressables.LoadAssetAsync<GameConfig>("cfg_gameConfig.asset");

        GameConfig isComplete = null;

        operation.Completed += (handle) =>
        {
            isComplete = handle.Result;
        };

        while (isComplete == null)
        {
            yield return null;
        }

        NUnit.Framework.Assert.IsNotNull(isComplete);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnAsteriodCollision()
    {
        var gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

        CommonPlayerShipInstallBindings<Asteroid>(gameConfig, gameConfig.GameElements[0].Prefab.gameObject);

        var profileService = Container.Resolve<PlayerProfileService>();
        profileService.SetTotalLives(3);
        var totallives = profileService.GetTotalLives();

        var ship = Container.Resolve<ShipMovement>();
        var asteroid = Container.Resolve<Asteroid>();
        asteroid.SetVisibility(true);

        ship.transform.position = asteroid.transform.position = new Vector3(0, 0, 0);

        yield return null;

        NUnit.Framework.Assert.Less(profileService.GetTotalLives(), totallives);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnSaucerCollision()
    {
        var gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

        CommonPlayerShipInstallBindings<EnemySaucer>(gameConfig, gameConfig.GameElements[3].Prefab.gameObject);

        var profileService = Container.Resolve<PlayerProfileService>();
        profileService.SetTotalLives(3);
        var totallives = profileService.GetTotalLives();

        var ship = Container.Resolve<ShipMovement>();
        var saucer = Container.Resolve<EnemySaucer>();
        saucer.SetVisibility(true);

        ship.transform.position = saucer.transform.position = new Vector3(0, 0, 0);

        yield return null;

        NUnit.Framework.Assert.Less(profileService.GetTotalLives(), totallives);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnEnemyProjectileCollision()
    {
        var gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

        CommonPlayerShipInstallBindings<Bullet>(gameConfig, gameConfig.EnemyProjectile.gameObject);

        var profileService = Container.Resolve<PlayerProfileService>();
        profileService.SetTotalLives(3);
        var totallives = profileService.GetTotalLives();

        var ship = Container.Resolve<ShipMovement>();
        var bullet = Container.Resolve<Bullet>();
        bullet.SetVisibility(true);

        ship.transform.position = bullet.transform.position = new Vector3(0, 0, 0);

        yield return null;

        NUnit.Framework.Assert.Less(profileService.GetTotalLives(), totallives);
    }

    private void CommonPlayerShipInstallBindings<TComponent>(GameConfig gameConfig, GameObject mockGameObject)
    {
        var cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.tag = "MainCamera";

        PreInstall();

        // Call Container.Bind methods

        Container.Bind<ShipMovement>().FromComponentInNewPrefab(gameConfig.PlayerShip).AsSingle();
        Container.Bind<TComponent>().FromComponentInNewPrefab(mockGameObject).AsSingle();

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

        PostInstall();
    }
}