using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling
{
    public class AudioConfig2D : AudioConfigBase<AudioConfig2D>
    {
        public AudioConfig2D(SoundPooler soundPooler, AudioClip audioClip) : base(soundPooler, audioClip)
        {
            SpacialBlend = 0f;
        }
    }
}
