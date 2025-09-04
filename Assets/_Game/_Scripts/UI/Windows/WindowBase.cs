using System;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        private IPersistantProgressService _progressService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            UnSubscribe();

        public void Inject(IPersistantProgressService progressService)
        {
            _progressService = progressService;
        }
        
        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(()=> Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnSubscribe(){}
    }
}