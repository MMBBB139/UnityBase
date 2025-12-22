using Sisus.Init;
using UnityEngine;

namespace _Project.Scripts.Core.SoundPooling
{
    [Service(typeof(SoundPooler), FindFromScene = true)]
    public class SoundPooler : MonoBehaviour
    {
        public AudioConfig2D New2DClip(AudioClip audioClip)
        {
            return new AudioConfig2D(this, audioClip);
        }

        public AudioConfig3D New3DClip(AudioClip audioClip)
        {
            return new AudioConfig3D(this, audioClip);
        }
        
        public IAudioPlayer Play(IAudioConfig audioConfig)
        {
            return null;
        }
    }
}
