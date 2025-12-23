using _Project.Scripts.Util.Scene;
using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts.Core.SceneLoading
{
    public class SceneUnloader : MonoBehaviour<SceneController>
    {
        [SerializeField] private bool withOverlay;
        [SerializeField] private SceneReference sceneRef;
        
        private SceneController _sceneController;
        
        protected override void Init(SceneController sceneController)
        {
            _sceneController = sceneController;
        }

        public void UnloadScene()
        {
            if (sceneRef.BuildIndex == 0)
            {
                Debug.LogError($"GameObject: {gameObject.name} from Scene: {gameObject.scene.name} " +
                               $"Tried to unload BootStrap. Skip Scene unloading");
                return;
            }
            
            _sceneController
                .NewStrategy()
                .Unload(sceneRef.BuildIndex)
                .WithOverlay(withOverlay)
                .Execute();
        }
    }
}
