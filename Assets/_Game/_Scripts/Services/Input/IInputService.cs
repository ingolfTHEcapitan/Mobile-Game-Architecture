using UnityEngine;

namespace _Game._Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
} 