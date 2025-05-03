using UnityEngine;
using _Game._Scripts.Services.Input;

namespace _Game._Scripts.Infrastructure
{
    public class Game
    {
        private IInputService inputService;
        
        public Game()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            if (Application.isEditor)
                inputService = new StandaloneInputService();
            else
                inputService = new MobileInputService();
        }
    }
}