using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Game._Scripts.Logic
{
    public class SaveTrigger: MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        private ISaveLoadService saveLoadService;

        public BoxCollider BoxCollider
        {
            get => _boxCollider;
            private set => _boxCollider = value; 
        }

        private void Awake()
        {
            saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            saveLoadService.SaveProgress();
            Debug.Log("ProgressSaved");
            gameObject.SetActive(false);
        }
    }
}