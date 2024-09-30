namespace Asteroids.Game.Core
{
    public interface IGameEntity
    {
        UnityEngine.GameObject GameObject { get; }

        void SetDirection(UnityEngine.Vector2 direction);
        void OnInitialize(int score);
        void EntityStart();
        void EntityUpdate();
        void EntityFixedUpdate();
        void DisposeEntity();
        void SetVisibility(bool isVisible);
    }
}