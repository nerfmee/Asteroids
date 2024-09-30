namespace Asteroids.Game.Core
{
    public interface IGameLoop
    {
        void AddGameEntity(IGameEntity gameEntity);
        void RemoveGameEntity(IGameEntity gameEntity);
        void OnStateChanged(IGameState state);
        void UpdateFrame();
        void FixedUpdateFrame();
    }
}