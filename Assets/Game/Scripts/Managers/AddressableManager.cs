using Asteroids.Game.Config;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Asteroids.Game.Management
{
    public class AddressableManager : MonoBehaviour
    {
        [SerializeField] private AssetReference _gameConfigRef;

        private ISignalService _signalService;
        private IAssetProvider _assetProvider;
        private IConfigCollectionService _configService;

        [Inject]
        private void Init(ISignalService signalService,
            IAssetProvider assetProvider,
            IConfigCollectionService game)
        {
            _signalService = signalService;
            _assetProvider = assetProvider;
            _configService = game;

            StartCoroutine(LoadAssetRoutine());
        }

        private IEnumerator LoadAssetRoutine()
        {
            _signalService.Publish(new GameStateUpdateSignal { Value = GameState.Loading });

            //Fake Delay
            yield return new WaitForSeconds(1f);

            _assetProvider.LoadAssetAsync<GameConfig>(_gameConfigRef, (asset) =>
            {
                _configService.SetGameConfig(asset);
                _signalService.Publish(new GameConfigLoadedSignal { Value = asset });
            });
        }
    }
}