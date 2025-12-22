using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling
{
    public abstract class AudioConfigBase<TSelf> : IAudioConfig where TSelf : AudioConfigBase<TSelf>
    {
        private readonly SoundPooler _soundPooler;
        
        public AudioClip AudioClip { get; set; }
        public AudioType AudioType { get; set; }
        public Vector3 Position { get; set; }
        public float SpacialBlend { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public bool IsBypassReverbZones { get; set; }
        public bool IsLooping { get; set; }

        protected AudioConfigBase(SoundPooler soundPooler, AudioClip audioClip)
        {
            _soundPooler = soundPooler;
            AudioClip = audioClip;
        }
        
        protected TSelf OnChannel(AudioType audioType)
        {
            AudioType = audioType;
            return (TSelf) this;
        }

        public TSelf AtPosition(Vector3 position)
        {
            Position = position;
            return (TSelf) this;
        }

        public TSelf Loop()
        {
            IsLooping = true;
            return (TSelf) this;
        }

        public IAudioPlayer Play() => _soundPooler.Play(this);
    }
}
