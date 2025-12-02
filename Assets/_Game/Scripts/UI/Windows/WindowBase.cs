using _Game.Scripts.Data.Player;
using _Game.Scripts.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private IPersistantProgressService _progressService;
        protected PlayerProgress Progress => _progressService.Progress;

        public void Construct(IPersistantProgressService progressService) => 
            _progressService = progressService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            UnSubscribe();

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(()=> Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnSubscribe(){}
    }
}