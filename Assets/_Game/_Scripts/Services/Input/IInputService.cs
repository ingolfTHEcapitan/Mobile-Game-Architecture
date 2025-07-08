using _Game._Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Game._Scripts.Services.Input
{
    public interface IInputService: IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
} 