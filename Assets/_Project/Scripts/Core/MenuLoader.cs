using System;
using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class MenuLoader : MonoBehaviour<SceneLoader>
    {
        [SerializeField] private string menuScene = "";
        
        private SceneLoader _sceneLoader;
        protected override void Init(SceneLoader argument)
        {
            _sceneLoader = argument;
        }

        private void Start()
        {
            _sceneLoader
                .NewStrategy()
                .Load(menuScene, true)
                .WithOverlay()
                .Execute();
        }
    }
}
