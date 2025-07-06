using UnityEngine;

namespace _Game._Scripts.Infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string HeroPath = "Prefabs/Hero";
        private const string HUDPath = "Prefabs/Hud";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        
        public void Exit()
        {
            
        }
        
        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject hero = Instantiate(HeroPath, at: initialPoint.transform.position);
            
            Instantiate(HUDPath);
            CameraFollow(hero);
        }

        private GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        private void CameraFollow(GameObject target)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(target);
        }
    }
}