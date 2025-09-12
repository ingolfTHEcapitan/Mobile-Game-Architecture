using _Game._Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Game._Scripts.Logic.Triggers
{
    public class SaveTrigger: TriggerBase
    {
        private ISaveLoadService _saveLoadService;

        public void Initialize(ISaveLoadService saveLoadService) => 
            _saveLoadService = saveLoadService;

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("ProgressSaved");
            gameObject.SetActive(false);
        }
    }
}