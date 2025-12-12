using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Game.Scripts.Services.AssetManagement
{
    public interface IAssetProvider: IService
    {
        void Initialize();
        Task<GameObject> Instantiate(string path);
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<GameObject> Instantiate(string path, Transform under);
        Task<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        Task<T> LoadAsync<T>(string assetAddress) where T : class;
        void CleanUp();
    }
}