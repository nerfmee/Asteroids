namespace Asteroids.Game.Core
{
    public interface IGameLoop
    {
        void AddGameEntity(IGameEntity gameEntity);
        void RemoveGameEntity(IGameEntity gameEntity);
        void UpdateFrame();
        void FixedUpdateFrame();
        void DisposeGameEntities();
    }
}