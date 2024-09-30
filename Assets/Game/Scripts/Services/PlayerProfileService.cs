using System;

namespace Asteroids.Game.Services
{
    public class PlayerProfileService : IPlayerProfileService
    {
        private int _currentScore;
        private int _totalLives;

        public void AddScore(int points)
        {
            _currentScore += points;
        }

        public int GetScore()
        {
            return _currentScore;
        }

        public int GetTotalLives()
        {
            return _totalLives;
        }

        public void OnScoreUpdated(Action<int> callback)
        {
            callback?.Invoke(_currentScore);
        }

        public void SetScore(int score)
        {
            _currentScore = score;
        }

        public void SetTotalLives(int totalLives)
        {
            _totalLives = totalLives;
        }
    }
}