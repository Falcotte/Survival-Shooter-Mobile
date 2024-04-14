using System.Collections.Generic;
using SurvivalShooter.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalShooter.Loading
{
    public class LoadingService : BaseService<ILoadingService>, ILoadingService
    {
        [SerializeField] private List<string> _permanentScenes;

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

        private void LoadPermanentScenes()
        {
            foreach(var scene in _permanentScenes)
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