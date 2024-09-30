using System;

namespace Asteroids.Game.Services
{
    public interface IPlayerProfileService
    {
        void SetScore(int score);
        void AddScore(int points);
        int GetScore();
        void OnScoreUpdated(Action<int> callback);
        void SetTotalLives(int totalLives);
        int GetTotalLives();
    }
}