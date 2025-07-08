using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.Input
{
    public class MobileInputService: InputService
    { 
        public override Vector2 Axis => SimpleInputAxis();
    }
}