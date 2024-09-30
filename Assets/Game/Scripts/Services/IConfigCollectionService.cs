using Asteroids.Game.Config;

namespace Asteroids.Game.Services
{
    public interface IConfigCollectionService
    {
        public GameConfig GameConfig { get; }

        void SetGameConfig(GameConfig gameConfig);
    }
}