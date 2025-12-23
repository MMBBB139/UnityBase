using _Project.Scripts.Util;
using _Project.Scripts.Util.Scene;
using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core.SceneLoading
{
    public class SceneLoader : MonoBehaviour<SceneController>
    {
        [SerializeField] private SceneReference sceneRef;
        [SerializeField] private bool withOverlay;
        
        private SceneController _sceneController;
        protected override void Init(SceneController argument)
        {
            _sceneController = argument;
        }

        public void TransitionToScene()
        {
            if (sceneRef.BuildIndex == 0)
            {
                Debug.LogError($"GameObject: {gameObject.name} from Scene: {gameObject.scene.name} " +
                               $"Tried to load BootStrap. Skip Scene loading");
                return;
            }
            
            _sceneController
                .NewStrategy()
                .Load(sceneRef.BuildIndex)
                .Unload(gameObject.scene.buildIndex)
                .WithOverlay()
                .Execute();
        }

        public void LoadSceneAdditive()
        {
            if (sceneRef.BuildIndex == 0)
            {
                Debug.LogError($"GameObject: {gameObject.name} from Scene: {gameObject.scene.name} " +
                               $"Tried to load BootStrap. Skip Scene loading");
                return;
            }
            
            _sceneController
                .NewStrategy()
                .Load(sceneRef.BuildIndex)
                .WithOverlay(withOverlay)
                .Execute();
        }
    }
}
