using Asteroids.Game.Services;
using Asteroids.Game.Signals;

namespace Game.Scripts.Services
{
    public class ScoreService
    {
        private IPlayerProfileService _playerProfileService;
        private ISignalService _signalService;

        public ScoreService(IPlayerProfileService playerProfileService, ISignalService signalService)
        {
            _playerProfileService = playerProfileService;
            _signalService = signalService;
        }

        public void AddScore(UpdateScoreSignal signal)
        {
            _playerProfileService.AddScore(signal.Value);
            _signalService.Publish(new DisplayScoreSignal { Value = _playerProfileService.GetScore() });
        }
    }
}