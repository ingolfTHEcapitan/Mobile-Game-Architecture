using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.Input
{
    public abstract class  InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire";

        public abstract Vector2 Axis { get;}

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Button);
        }
    }
}