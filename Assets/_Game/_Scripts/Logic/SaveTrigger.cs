using System;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game._Scripts.Logic
{
        
    public class SaveTrigger: MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        private ISaveLoadService saveLoadService;

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

        private void OnDrawGizmos()
        {
            if (_boxCollider is null)
                return; 
            
            Gizmos.color = new Color32(15, 161, 49, 130);
            Gizmos.DrawCube(transform.position + _boxCollider.center, _boxCollider.size);
        }
    }
}