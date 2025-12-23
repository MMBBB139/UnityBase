using _Project.Scripts.Core.SoundPooling.Interface;

namespace _Project.Scripts.Core.SoundPooling.Implement
{
    public class EmptyAudioPlayer : IAudioPlayer
    {
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
