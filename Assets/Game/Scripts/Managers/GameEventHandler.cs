using Asteroids.Game.Signals;
using Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Management
{
    public class GameEventHandler : MonoBehaviour
    {
        private ISignalService _signalService;
        private PlayerLifeService _playerLifeService;
        private ScoreService _scoreService;

        [Inject]
        private void InitServices(ISignalService signalService, PlayerLifeService playerLifeService, ScoreService scoreService)
        {
            _signalService = signalService;
            _playerLifeService = playerLifeService;
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            _signalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
            _signalService.Subscribe<UpdateScoreSignal>(_scoreService.AddScore);
            _signalService.Subscribe<PlayerDiedSignal>(_playerLifeService.HandlePlayerDeath);
        }

        private void OnDisable()
        {
            _signalService.RemoveSignal<GameConfigLoadedSignal>(OnGameConfigLoaded);
            _signalService.RemoveSignal<UpdateScoreSignal>(_scoreService.AddScore);
            _signalService.RemoveSignal<PlayerDiedSignal>(_playerLifeService.HandlePlayerDeath);
        }

        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            _playerLifeService.SetTotalLives(signal.Value.TotalLives);
        }
    }

}