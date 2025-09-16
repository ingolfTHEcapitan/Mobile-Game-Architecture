using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider: IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Transform under);
        T Load<T>(string path) where T : Object;
    }
}