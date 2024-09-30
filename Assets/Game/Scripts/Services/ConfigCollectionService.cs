using Asteroids.Game.Config;

namespace Asteroids.Game.Services
{
    public class ConfigCollectionService : IConfigCollectionService
    {
        public GameConfig GameConfig { get; private set; }

        public void SetGameConfig(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }
}