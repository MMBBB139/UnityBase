using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling
{
    public class AudioConfig3D : AudioConfigBase<AudioConfig3D>
    {
        public AudioConfig3D(SoundPooler soundPooler, AudioClip audioClip) : base(soundPooler, audioClip) { }
        
        public AudioConfig3D WithMinDistance(float minDistance)
        {
            MinDistance = minDistance;
            return this;
        }
        
        public AudioConfig3D WithMaxDistance(float maxDistance)
        {
            MaxDistance = maxDistance;
            return this;
        }

        public AudioConfig3D BypassReverbZones()
        {
            IsBypassReverbZones = true;
            return this;
        }
    }
}
