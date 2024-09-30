using Asteroids.Game.Config;
using UnityEngine.AddressableAssets;

namespace Asteroids.Game.Services
{
    public interface IAssetProvider
    {
        void LoadAssetAsync<TAsset>(string assetPath, System.Action<TAsset> callback);

        void LoadAssetAsync<TAsset>(AssetReference assetReference, System.Action<TAsset> callback);
    }

    public interface IConfigCollectionService
    {
        public GameConfig GameConfig { get; }
        void SetGameConfig(GameConfig gameConfig);
    }

    public class GameConfigurationFacade : IConfigCollectionService
    {
        public GameConfig GameConfig { get; private set; }

        public void SetGameConfig(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }
}