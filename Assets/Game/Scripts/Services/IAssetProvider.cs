using UnityEngine.AddressableAssets;

namespace Asteroids.Game.Services
{
    public interface IAssetProvider
    {
        void LoadAssetAsync<TAsset>(string assetPath, System.Action<TAsset> callback);

        void LoadAssetAsync<TAsset>(AssetReference assetReference, System.Action<TAsset> callback);
    }
}