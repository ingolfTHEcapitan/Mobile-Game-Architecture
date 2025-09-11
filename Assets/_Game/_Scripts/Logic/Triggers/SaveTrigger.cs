using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Game._Scripts.Logic.Triggers
{
    public class SaveTrigger: TriggerBase
    {
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
    }
}