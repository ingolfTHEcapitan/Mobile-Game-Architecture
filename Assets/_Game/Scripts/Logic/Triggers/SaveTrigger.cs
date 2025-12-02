using _Game.Scripts.Services.SaveLoad;
using UnityEngine;

namespace _Game.Scripts.Logic.Triggers
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