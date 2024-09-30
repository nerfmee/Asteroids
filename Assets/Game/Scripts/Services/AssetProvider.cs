using System;
using UnityEngine.AddressableAssets;

namespace Asteroids.Game.Services
{
    public class AssetProvider : IAssetProvider
    {
        public void LoadAssetAsync<TAsset>(AssetReference assetReference, Action<TAsset> callback)
        {
            var operation = assetReference.LoadAssetAsync<TAsset>();
            operation.Completed += (handler) =>
            {
                if (handler.IsDone)
                {
                    callback.Invoke(handler.Result);
                }
            };
        }

        public void LoadAssetAsync<TAsset>(string assetPath, Action<TAsset> callback)
        {
            var operation = Addressables.LoadAssetAsync<TAsset>(assetPath);
            operation.Completed += (handler) =>
            {
                if (handler.IsDone)
                    callback.Invoke(handler.Result);
            };
        }
    }
}