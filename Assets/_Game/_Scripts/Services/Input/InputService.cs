using UnityEngine;

namespace _Game._Scripts.Services.Input
{
    public abstract class  InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire1";

        public abstract Vector2 Axis { get;}

        protected static Vector2 SimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public bool IsAttackButtonUp() => 
            SimpleInput.GetButtonUp(Button);
    }
}