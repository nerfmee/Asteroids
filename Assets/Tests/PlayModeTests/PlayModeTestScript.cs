using NUnit.Framework;
using System.Collections;
using Asteroids.Game.Config;
using Asteroids.Game.Management;
using Asteroids.Game.Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;

public class PlayModeTestScript
{
    private GameObject mockEnvironment;
    private GameConfig gameConfig;

    [SetUp]
    public void Setup()
    {
        mockEnvironment = MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Tests/MockEnvironment.prefab"));
        gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");
        SignalService.Publish(new GameConfigLoadedSignal { Value = gameConfig });
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(mockEnvironment);
        gameConfig = null;
    }

    [UnityTest]
    public IEnumerator UT_PlayModeLoadAddressableTest()
    {
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

        Assert.IsNotNull(isComplete);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnAsteriodCollision()
    {
        mockEnvironment.GetComponentInChildren<MainManager>().SetTotalLives(gameConfig.TotalLives);

        var ship = MonoBehaviour.Instantiate(gameConfig.PlayerShip);
        var asteriod = MonoBehaviour.Instantiate(gameConfig.GameElements[0].Prefab);
        asteriod.SetVisibility(true);

        ship.transform.position = asteriod.transform.position = new Vector3(0, 0, 0);

        yield return null;
        yield return null;
        yield return null;

        Object.Destroy(ship);
        Assert.Less(mockEnvironment.GetComponentInChildren<MainManager>().TotalLives, gameConfig.TotalLives);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnSaucerCollision()
    {
        mockEnvironment.GetComponentInChildren<MainManager>().SetTotalLives(gameConfig.TotalLives);

        var ship = MonoBehaviour.Instantiate(gameConfig.PlayerShip);
        var saucer = MonoBehaviour.Instantiate(gameConfig.GameElements[3].Prefab);
        saucer.SetVisibility(true);

        ship.transform.position = saucer.transform.position = new Vector3(0, 0, 0);

        yield return null;
        yield return null;
        yield return null;

        Object.Destroy(ship);

        Assert.Less(mockEnvironment.GetComponentInChildren<MainManager>().TotalLives, gameConfig.TotalLives);
    }

    [UnityTest]
    public IEnumerator UT_TestDeductPlayerLifeOnEnemyProjectileCollision()
    {
        mockEnvironment.GetComponentInChildren<MainManager>().SetTotalLives(gameConfig.TotalLives);

        var ship = MonoBehaviour.Instantiate(gameConfig.PlayerShip);
        var projectile = MonoBehaviour.Instantiate(gameConfig.EnemyProjectile);
        projectile.SetVisibility(true);

        ship.transform.position = projectile.transform.position = new Vector3(5, 5, 0);

        yield return null;
        Object.Destroy(ship);

        Assert.Less(mockEnvironment.GetComponentInChildren<MainManager>().TotalLives, gameConfig.TotalLives);
    }

    [UnityTest]
    public IEnumerator UT_TestGameOverOnAllLivesLost()
    {
        mockEnvironment.GetComponentInChildren<MainManager>().SetTotalLives(1);

        var ship = MonoBehaviour.Instantiate(gameConfig.PlayerShip);
        var projectile = MonoBehaviour.Instantiate(gameConfig.EnemyProjectile);
        projectile.SetVisibility(true);

        ship.transform.position = projectile.transform.position = new Vector3(-5, -5, 0);

        yield return null;

        Object.Destroy(ship);
        Assert.AreEqual(MainManager.CurrentGameState, GameState.GameOver);
    }
}