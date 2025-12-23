using System;
using System.Collections;
using _Project.Scripts.Core.SoundPooling.Interface;
using _Project.Scripts.Util;
using _Project.Scripts.Util.GameObject.Extension;
using Sisus.Init;
using UnityEngine;
using AudioType = _Project.Scripts.Core.SoundPooling.Interface.AudioType;

namespace _Project.Scripts.Core.SoundPooling.Implement
{
    public class PooledAudioSource : MonoBehaviour<AudioPooler>, IAudioPlayer
    {
        public event Action OnAudioFinished;
        public int Priority { get; private set; }
        public AudioType AudioType { get; private set; }
        public AudioClip Clip { get; private set; }
        private AudioSource _audioSource;
        private AudioPooler _pooler;

        private Coroutine _playingCoroutine;
        
        protected override void Init(AudioPooler pooler)
        {
            _pooler = pooler;
        }
        
        protected override void OnAwake()
        {
            _audioSource = gameObject.GetOrAdd<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Initialize(IAudioConfig audioConfig)
        {
            AudioType = audioConfig.AudioType;
            Clip = audioConfig.Clip;
            _audioSource.clip = audioConfig.Clip;
            _audioSource.outputAudioMixerGroup = audioConfig.AudioMixerGroup;
            Priority = audioConfig.Priority;
            transform.position = audioConfig.Position;
            _audioSource.spatialBlend = audioConfig.SpatialBlend;
            _audioSource.loop = audioConfig.Loop;
            _audioSource.minDistance = audioConfig.MinDistance;
            _audioSource.maxDistance = audioConfig.MaxDistance;
            _audioSource.bypassReverbZones = audioConfig.IsBypassReverbZones;
        }

        public void Play()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
            }
            
            _audioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForAudioCompletion());
        }

        private IEnumerator WaitForAudioCompletion()
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);
            OnAudioFinished?.Invoke();
            _pooler.ReturnToPool(this);
        }

        public void Stop()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }
            _audioSource.Stop();
            OnAudioFinished?.Invoke();
            _pooler.ReturnToPool(this);
        }

        public void Pause()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }

            _audioSource.Pause();
        }

        public void Resume()
        {
            Play();
        }
    }
}
