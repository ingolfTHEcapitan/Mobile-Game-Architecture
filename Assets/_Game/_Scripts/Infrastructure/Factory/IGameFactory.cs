using _Game._Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}