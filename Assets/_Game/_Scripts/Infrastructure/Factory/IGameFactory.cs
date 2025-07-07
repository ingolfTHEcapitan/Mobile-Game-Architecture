using UnityEngine;

namespace _Game._Scripts.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}