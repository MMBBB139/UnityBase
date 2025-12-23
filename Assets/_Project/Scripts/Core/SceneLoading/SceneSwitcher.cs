using _Project.Scripts.Util;
using _Project.Scripts.Util.Scene;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core.SceneLoading
{
    public class SceneSwitcher : MonoBehaviour<SceneController>
    {
        [SerializeField] private SceneReference sceneRef;
        
        private SceneController _sceneController;
        protected override void Init(SceneController argument)
        {
            _sceneController = argument;
        }

        public void LoadScene()
        {
            _sceneController
                .NewStrategy()
                .Load(sceneRef.BuildIndex)
                .Unload(gameObject.scene.buildIndex)
                .WithOverlay()
                .Execute();
        }
    }
}
