using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling
{
    public enum AudioType
    {
        Sfx,
        UI,
        Music,
        Master
    }
    
    public interface IAudioConfig
    {
        public AudioClip AudioClip { get; set; }
        public AudioType AudioType { get; set; } 
        public Vector3 Position { get; set; }
        public float SpacialBlend { get; set; }
        public bool IsLooping { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public bool IsBypassReverbZones { get; set; }
    }
}
