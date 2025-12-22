using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts.Core.SceneLoading
{
    public class MainMenuLoader : MonoBehaviour<SceneController>
    {
        [SerializeField] private string menuScene = "";
        
        private SceneController _sceneController;
        protected override void Init(SceneController argument)
        {
            _sceneController = argument;
        }

        protected override void OnAwake()
        {
            _sceneController
                .NewStrategy()
                .Load(menuScene)
                .Execute();
        }
    }
}
