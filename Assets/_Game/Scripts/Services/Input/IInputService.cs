using UnityEngine;

namespace _Game.Scripts.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
} 