using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class EntityContainer : MonoBehaviour
    {
        private IGame game;
        private static EntityContainer instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            game = new GameLoop();
        }

        public static void AddEntity(IGameEntity entity)
        {
            if (instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (instance.game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.Initialize();

            instance.game.AddGameEntity(entity);
        }

        public static void RemoveEntity(IGameEntity entity)
        {
            if (instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (instance.game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            instance.game.RemoveGameEntity(entity);
        }

        private void Update()
        {
            if (instance.game != null)
                instance.game.UpdateGame();
        }
    }
}