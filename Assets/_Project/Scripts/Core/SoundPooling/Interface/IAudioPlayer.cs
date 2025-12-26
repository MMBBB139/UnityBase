using System;

namespace _Project.Scripts.Core.SoundPooling.Interface
{
    public interface IAudioPlayer
    {
        event Action OnAudioFinished;
        void Play();
        void Stop();
        void FadeVolume(float volume, float duration = 0);
        void Pause();
        void Resume();
    }
}
