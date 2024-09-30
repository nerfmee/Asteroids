using Asteroids.Game.Config;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using Zenject;

namespace Asteroids.Game.Core
{
    public interface IGameState
    {
        void Execute();
    }

    public class BaseGameState : IGameState
    {
        private IConfigCollectionService _configService;
        protected IPlayerProfileService _playerProfileService;
        protected ISignalService _signalService;

        [Inject]
        private void InitService(IConfigCollectionService configService,
            IPlayerProfileService playerProfileService,
            ISignalService signalService)
        {
            _configService = configService;
            _playerProfileService = playerProfileService;
            _signalService = signalService;
        }

        protected GameConfig GameConfig => _configService.GameConfig;

        public virtual void Execute()
        {
        }
    }
}