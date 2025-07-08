using _Game._Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider: IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}