using _Game._Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Elements
{
    public class OpenWindowButton: MonoBehaviour
    {
        [SerializeField] private WindowId _windowId;
        [SerializeField] private Button _button;
        
        private IWindowService _windowService;

        private void Start() => 
            _button.onClick.AddListener(OpenWindow);

        public void Initialize(IWindowService windowService) => 
            _windowService = windowService;

        private void OpenWindow() => 
            _windowService.Open(_windowId);
    }
}