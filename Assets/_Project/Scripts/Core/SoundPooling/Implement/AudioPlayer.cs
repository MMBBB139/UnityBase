using _Project.Scripts.Core.SoundPooling.Interface;
using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling.Implement
{
    public class AudioPlayer : IAudioPlayer
    {
        private PooledAudioSource _audioSource;
        private Coroutine _playingCoroutine;
        
        public AudioPlayer(PooledAudioSource audioSource)
        {
            _audioSource = audioSource;
            
            _audioSource.OnAudioFinished += OnAudioFinished;
        }

        private void OnAudioFinished()
        {
            if (_audioSource == null)
            {
                return;
            }
            
            _audioSource.OnAudioFinished -= OnAudioFinished;
            _audioSource = null;
        }

        public void Play()
        {
            if (_audioSource == null)
            {
                Debug.LogWarning("AudioPlayer Trying to Play from an Expired Source");
                return;
            }
            
            _audioSource.Play();
        }

        public void Stop()
        {
            if (_audioSource == null)
            {
                Debug.LogWarning("AudioPlayer Trying to Stop an Expired Source");
                return;
            }
            _audioSource.Stop();
        }

        public void Pause()
        {
            if (_audioSource == null)
            {
                Debug.LogWarning("AudioPlayer Trying to Pause an Expired Source");
                return;
            }
            _audioSource.Pause();
        }

        public void Resume()
        {
            if (_audioSource == null)
            {
                Debug.LogWarning("AudioPlayer Trying to Resume an Expired Source");
                return;
            }
            _audioSource.Play();
        }
    }
}
