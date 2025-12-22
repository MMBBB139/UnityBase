using Sisus.Init;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core.SceneLoading
{
    public class SceneSwitcher : MonoBehaviour<SceneController>
    {
        [SerializeField] private string scene = "";
        
        private SceneController _sceneController;
        protected override void Init(SceneController argument)
        {
            _sceneController = argument;
        }

        public void LoadScene()
        {
            _sceneController
                .NewStrategy()
                .Load(scene)
                .Unload(gameObject.scene.name)
                .WithOverlay()
                .Execute();
        }
    }
}
