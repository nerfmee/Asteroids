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
        [SerializeField] private GameEntity enemyProjectile;
        [SerializeField] private GameObject playerShip;
        [SerializeField] private Wave[] enemyWaves;

        public int TotalLives { get => totalLives; }
        public GameplayElement[] GameElements { get => gameElements; }
        public GameEntity PlayerProjectile { get => playerProjectile; }
        public GameEntity EnemyProjectile { get => enemyProjectile; }
        public GameObject PlayerShip { get => playerShip; }
        public Wave[] EnemyWaves { get => enemyWaves; }
    }

    [System.Serializable]
    public class PrefabElement
    {
        [SerializeField] private string id;
        [SerializeField] private GameEntity prefab;

        public string Id { get => id; set => id = value; }
        public GameEntity Prefab { get => prefab; set => prefab = value; }
    }

    [System.Serializable]
    public class GameplayElement : PrefabElement
    {
        [SerializeField] private int score;
        [SerializeField] private string[] spawnOnDestroy;

        public int Score { get => score; }
        public string[] SpawnOnDestroy { get => spawnOnDestroy; }
    }

    [System.Serializable]
    public class Wave
    {
        [SerializeField] private int count;
        [SerializeField] private string[] enemies;
        [SerializeField] private float delay;
        [SerializeField] private bool clamp;

        public int Count { get => count; }
        public string[] Enemies { get => enemies; }
        public float Delay { get => delay; }
        public bool Clamp { get => clamp; }
    }
}