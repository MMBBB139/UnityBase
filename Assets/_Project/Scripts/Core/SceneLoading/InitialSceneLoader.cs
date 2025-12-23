using System.Collections.Generic;
using _Project.Scripts.Util;
using _Project.Scripts.Util.Scene;
using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts.Core.SceneLoading
{
    public class InitialSceneLoader : MonoBehaviour<SceneController>
    {
        [SerializeField] private List<SceneReference> sceneRefs;
        [SerializeField] private bool withOverlay;
        
        private SceneController _sceneController;
        protected override void Init(SceneController argument)
        {
            _sceneController = argument;
        }

        protected override void OnAwake()
        {
            SceneController.SceneLoadingStrategy loadingStrategy = _sceneController.NewStrategy();

            foreach (var scene in sceneRefs)
            {
                loadingStrategy.Load(scene.BuildIndex);
            }

            loadingStrategy
                .WithOverlay(withOverlay)
                .Execute();
        }
    }
}
