using Asteroids.Game.Config;
using Asteroids.Game.Signals;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Asteroids.Game.Management
{
    public class AddressableManager : MonoBehaviour
    {
        [SerializeField] private string _assetPath;

        private IEnumerator Start()
        {
            SignalService.Publish(new GameStateUpdateSignal { Value = GameState.Loading });

            //Fake Delay
            yield return new WaitForSeconds(1f);

            LoadAddressableAsync<GameConfig>(_assetPath, (handle) =>
            {
                SignalService.Publish(new GameConfigLoadedSignal { Value = handle.Result });
            });
        }

        private void LoadAddressableAsync<T>(string assetPath, System.Action<AsyncOperationHandle<T>> callback)
        {
            var operation = Addressables.LoadAssetAsync<T>(assetPath);
            operation.Completed += (handler) =>
            {
                if (handler.IsDone)
                    callback.Invoke(handler);
            };
        }
    }
}