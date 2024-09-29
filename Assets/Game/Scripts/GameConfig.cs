using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Config
{
    [CreateAssetMenu(fileName = "GameConfig.asset", menuName = "Asteroids/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int totalLives = 3;
        [SerializeField] private GameplayElement[] gameElements;
        [SerializeField] private GameEntity playerProjectile;
        [SerializeField] private GameEntity playerLaser;
        [SerializeField] private GameEntity enemyProjectile;
        [SerializeField] private GameObject playerShip;
        [SerializeField] private Wave[] enemyWaves;

        public int TotalLives => totalLives;
        public GameplayElement[] GameElements => gameElements;
        public GameEntity PlayerProjectile => playerProjectile;
        public GameEntity PlayerLaser => playerLaser;
        public GameEntity EnemyProjectile => enemyProjectile;
        public GameObject PlayerShip => playerShip;
        public Wave[] EnemyWaves => enemyWaves;
    }

    [System.Serializable]
    public class PrefabElement
    {
        [SerializeField] private string id;
        [SerializeField] private GameEntity prefab;

        public string Id => id;
        public GameEntity Prefab => prefab;
    }

    [System.Serializable]
    public class GameplayElement : PrefabElement
    {
        [SerializeField] private int score;
        [SerializeField] private string[] spawnOnDestroy;

        public int Score => score;
        public string[] SpawnOnDestroy => spawnOnDestroy;
    }

    [System.Serializable]
    public class Wave
    {
        [SerializeField] private int count;
        [SerializeField] private string[] enemies;
        [SerializeField] private float delay;
        [SerializeField] private bool clamp;

        public int Count => count;
        public string[] Enemies => enemies;
        public float Delay => delay;
        public bool Clamp => clamp;
    }
}