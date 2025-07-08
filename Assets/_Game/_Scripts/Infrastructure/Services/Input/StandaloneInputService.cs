using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.Input
{
    public class StandaloneInputService: InputService
    { 
        public override Vector2 Axis
        {
            get
            {
                // Получаем ввод с джойстика на экране.
                Vector2 axis = SimpleInputAxis();
                
                // Если он недоступен, получаем ввод методами Unity с клавиатуры.
                if (axis == Vector2.zero)
                    axis = UnityAxis();
                return axis;
            }
        }
        
        private static Vector2 UnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }
    }
}