namespace Asteroids.Game.Core
{
    public interface IGame
    {
        void AddGameEntity(IGameEntity gameEntity);
        void RemoveGameEntity(IGameEntity gameEntity);
        void OnStateChanged(IGameState state);
        void UpdateGame();
        void OnFixedUpdate();
    }
}