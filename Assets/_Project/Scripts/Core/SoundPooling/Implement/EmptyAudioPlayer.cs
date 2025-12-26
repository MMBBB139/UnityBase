using System;
using _Project.Scripts.Core.SoundPooling.Interface;

namespace _Project.Scripts.Core.SoundPooling.Implement
{
    public class EmptyAudioPlayer : IAudioPlayer
    {
        public event Action OnAudioFinished;

        public void FadeVolume(float volume, float duration = 0)
        {
            //noop
        }

        public void Play()
        {
            //noop
        }

        public void Stop()
        {
            //noop
        }

        public void Pause()
        {
            //noop
        }

        public void Resume()
        {
            //noop
        }
    }
}
