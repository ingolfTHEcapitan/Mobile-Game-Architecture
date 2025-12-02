using UnityEngine;

namespace _Game.Scripts.Services.Input
{
    public class MobileInputService: InputService
    { 
        public override Vector2 Axis => SimpleInputAxis();
    }
}