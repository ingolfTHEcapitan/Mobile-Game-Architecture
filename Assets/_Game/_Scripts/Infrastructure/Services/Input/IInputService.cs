using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
} 