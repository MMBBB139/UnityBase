using _Project.Scripts.Core.SoundPooling.Interface;

namespace _Project.Scripts.Core.SoundPooling
{
    public partial class AudioPooler : IAudioPoolSceneController
    {
        public void FadeAllVolumeFromScene(int sceneBuildIndex, float volume, float duration)
        {
            if (activeSourcesBySceneIndex.TryGetValue(sceneBuildIndex, out var audioSources))
            {
                foreach (var audioSource in audioSources)
                {
                    audioSource.FadeVolume(volume, duration);
                }
            }
        }
    }
}
