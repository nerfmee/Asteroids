using Asteroids.Game.Management;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Core
{
    public abstract class GameEntity : MonoBehaviour, IGameEntity
    {
        private Vector2 _direction;

        public GameObject GameObject => gameObject;
        public int DieScore { get; private set; }

        protected Vector3 MoveDirection => _direction;

        private IGameContainer _gameContainer;
        protected GameEntitySpawnService _spawnService;
        protected ISignalService _signalService;

        [Inject]
        private void Init(IGameContainer gameContainer,
            GameEntitySpawnService spawnManager,
            ISignalService signalService)
        {
            _gameContainer = gameContainer;
            _spawnService = spawnManager;
            _signalService = signalService;
            _gameContainer.AddEntity(this);
        }

        public virtual void EntityStart()
        {
        }

        public virtual void EntityUpdate()
        {
        }

        public virtual void EntityFixedUpdate()
        {
        }

        public virtual void DisposeEntity()
        {
            _gameContainer.RemoveEntity(this);
            Destroy(gameObject);
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void OnInitialize(int score)
        {
            DieScore = score;
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, GameEntity>
        {
        }
    }

    public class GameEntityFactory : IFactory<UnityEngine.Object, GameEntity>
    {
        private readonly DiContainer _container;

        public GameEntityFactory(DiContainer container)
        {
            _container = container;
        }

        public GameEntity Create(Object param)
        {
            return _container.InstantiatePrefabForComponent<GameEntity>(param);
        }
    }
}