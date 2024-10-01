using Asteroids.Game.Management;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;

namespace Game.Scripts.Services
{
    public class PlayerLifeService
    {
        private IPlayerProfileService _playerProfileService;
        private ISignalService _signalService;

        public PlayerLifeService(IPlayerProfileService playerProfileService, ISignalService signalService)
        {
            _playerProfileService = playerProfileService;
            _signalService = signalService;
        }

        public void HandlePlayerDeath(PlayerDiedSignal signal)
        {
            var lives = _playerProfileService.GetTotalLives() - 1;
            if (lives <= 0)
            {
                _signalService.Publish(new GameStateUpdateSignal { Value = GameState.GameOver });
            }
            else
            {
                _signalService.Publish(new UpdatePlayerLivesSignal { Value = lives });
                _signalService.Publish<PlayerReviveSignal>();
            }
            _playerProfileService.SetTotalLives(lives);
        }

        public void SetTotalLives(int lives)
        {
            _playerProfileService.SetTotalLives(lives);
        }
    }
}