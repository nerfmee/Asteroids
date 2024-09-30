using Asteroids.Game.Core;

namespace Asteroids.Game.Services
{
    public interface IGameContainer 
    {
        void AddEntity(IGameEntity entity);
        void RemoveEntity(IGameEntity entity);
    }
}