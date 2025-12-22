using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.UI.Interfaces;
using _Project.Scripts.Util;
using Sisus.Init;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core
{
    [Service(typeof(SceneLoader), FindFromScene = true)]
    public class SceneLoader : MonoBehaviour<ITransition>
    {
        private ITransition _loadingOverlay;
        
        protected override void Init(ITransition argument)
        {
            _loadingOverlay = argument;
        }
        
        private readonly HashSet<string> _loadedScenes = new();
        private bool _isBusy;

        protected override void OnAwake()
        {
            for (int index = 0; index < SceneManager.sceneCount; index++)
            {
                string sceneName = SceneManager.GetSceneAt(index).name;

                if (sceneName.Equals(SceneDatabase.BootStrap))
                {
                    continue;
                }

                _loadedScenes.Add(sceneName);
            }
        }

        public SceneLoadingStrategy NewStrategy()
        {
            return new SceneLoadingStrategy(this);
        }
        
        private Coroutine ExecuteLoadingStrategy(SceneLoadingStrategy sceneLoadingStrategy)
        {
            if (_isBusy)
            {
                Debug.LogWarning("SceneLoader is busy. Cannot load new strategy.");
                return null;
            }

            _isBusy = true;
            return StartCoroutine(ChangeSceneRoutine(sceneLoadingStrategy));
        }

        private IEnumerator ChangeSceneRoutine(SceneLoadingStrategy sceneLoadingStrategy)
        {
            if (sceneLoadingStrategy.Overlay)
            {
                _loadingOverlay.Show();
                yield return new WaitForSeconds(_loadingOverlay.TransitionDuration);
            }

            if (sceneLoadingStrategy.ClearUnusedAssets)
            {
                yield return CleanUpUnusedAssetsRoutine();
            }
            
            foreach(var sceneName in sceneLoadingStrategy.ScenesToUnload)
            {
                yield return UnloadSceneRoutine(sceneName);
            }

            foreach (var sceneName in sceneLoadingStrategy.ScenesToLoad)
            {
                if (_loadedScenes.Contains(sceneName))
                {
                    Debug.LogWarning($"Scene {sceneName} is already loaded. Skipping.");
                    continue;
                }
                yield return AdditiveLoadRoutine(sceneName, sceneName.Equals(sceneLoadingStrategy.ActiveSceneName));
            }
            
            if (sceneLoadingStrategy.Overlay)
            {
                _loadingOverlay.Hide();
                yield return new WaitForSeconds(_loadingOverlay.TransitionDuration);
            }
            _isBusy = false;
        }

        private IEnumerator CleanUpUnusedAssetsRoutine()
        {
            AsyncOperation cleanUpOp = Resources.UnloadUnusedAssets();
            while (!cleanUpOp.isDone)
            {
                yield return null;
            }
            
        }

        private IEnumerator AdditiveLoadRoutine(string sceneName, bool setActive = false)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("Scene name is empty. Skip Loading Scene.");
                yield break;
            }
            
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            if(loadOp == null) yield break;
            
            loadOp.allowSceneActivation = false;
            
            while (loadOp.progress < 0.9f)
            {
                yield return null;
            }
            
            loadOp.allowSceneActivation = true;
            
            while (!loadOp.isDone)
            {
                yield return null;
            }

            if (setActive)
            {
                Scene newScene = SceneManager.GetSceneByName(sceneName);

                if (newScene.IsValid() && newScene.isLoaded)
                {
                    SceneManager.SetActiveScene(newScene);
                }
            }
            
            _loadedScenes.Add(sceneName);
        }

        private IEnumerator UnloadSceneRoutine(string sceneName)
        {
            if (!_loadedScenes.Contains(sceneName) || string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded. Skipping.");
                yield break;
            }
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);

            if (unloadOp == null)
            {
                Debug.LogWarning($"Scene {sceneName} failed to load.");
                yield break;
            }

            while (!unloadOp.isDone)
            {
                yield return null;
            }

            _loadedScenes.Remove(sceneName);
        }
        
        #region Scene Loading Strategy
        public class SceneLoadingStrategy
        {
            public HashSet<string> ScenesToLoad { get; } = new();
            public List<string> ScenesToUnload { get; } = new();
            public string ActiveSceneName { get; private set; } = "";
            public bool ClearUnusedAssets { get; private set; } = false;
            public bool Overlay { get; private set; } = false;
            
            private readonly SceneLoader _loader;
            
            public SceneLoadingStrategy(SceneLoader loader)
            {
                _loader = loader;
            }
            
            public SceneLoadingStrategy Load(string sceneName, bool setActive = false)
            {
                ScenesToLoad.Add(sceneName);
                ActiveSceneName = setActive ? sceneName : ActiveSceneName;
                return this;
            }
            
            public SceneLoadingStrategy Unload(string sceneName)
            {
                ScenesToUnload.Add(sceneName);
                return this;
            }
            
            public SceneLoadingStrategy WithOverlay()
            {
                Overlay = true;
                return this;
            }
            
            public SceneLoadingStrategy WithClearUnusedAssets()
            {
                ClearUnusedAssets = true;
                return this;
            }
            

            public Coroutine Execute()
            {
                return _loader.ExecuteLoadingStrategy(this);
            }
        }
#endregion
    }
}


