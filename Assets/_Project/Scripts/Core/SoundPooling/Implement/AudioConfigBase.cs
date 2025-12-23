using _Project.Scripts.Core.SoundPooling.Interface;
using UnityEngine;
using UnityEngine.Audio;
using AudioType = _Project.Scripts.Core.SoundPooling.Interface.AudioType;

namespace _Project.Scripts.Core.SoundPooling.Implement
{
    public abstract class AudioConfigBase<TSelf> : IAudioConfig where TSelf : AudioConfigBase<TSelf>
    {
        private readonly AudioPooler _audioPooler;
        
        public AudioClip Clip { get; set; }
        public AudioType AudioType { get; set; }
        public int Priority { get; set; } = 1;
        public AudioMixerGroup AudioMixerGroup { get; set; }
        public Vector3 Position { get; set; }
        public float SpatialBlend { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public bool IsBypassReverbZones { get; set; }
        public bool Loop { get; set; }

        protected AudioConfigBase(AudioPooler audioPooler, AudioClip audioClip)
        {
            _audioPooler = audioPooler;
            Clip = audioClip;
            
            // Default mixer group
            AudioMixerGroup = _audioPooler.GetMixerFor(AudioType.Sfx);
        }
        
        public TSelf OnChannel(AudioType audioType)
        {
            AudioType = audioType;
            AudioMixerGroup = _audioPooler.GetMixerFor(audioType);
            return (TSelf) this;
        }

        public TSelf LoopAudio()
        {
            Loop = true;
            return (TSelf) this;
        }

        public TSelf AddPriority(int priority)
        {
            Priority = priority;
            return (TSelf)this;
        }

        public TSelf MarkFrequent()
        {
            Priority = 0;
            return (TSelf)this;
        }

        public IAudioPlayer Play() => _audioPooler.Play(this);
    }
}
