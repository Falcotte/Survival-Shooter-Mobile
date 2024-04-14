using System.Collections.Generic;
using SurvivalShooter.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalShooter.Loading
{
    public class LoadingService : BaseService<ILoadingService>, ILoadingService
    {
        [SerializeField] private List<string> permanentScenes;

        protected override void Awake()
        {
            base.Awake();

            LoadPermanentScenes();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SetActiveScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SetActiveScene;
        }

        public void LoadPermanentScenes()
        {
            foreach(var scene in permanentScenes)
            {
                if(!IsSceneLoaded(scene))
                {
                    Debug.Log($"Loading {scene}");
                    SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                }
            }
        }

        private void SetActiveScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.SetActiveScene(scene);
        }

        private bool IsSceneLoaded(string sceneName)
        {
            var sceneCount = SceneManager.sceneCount;

            for(int i = 0; i < sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == sceneName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}