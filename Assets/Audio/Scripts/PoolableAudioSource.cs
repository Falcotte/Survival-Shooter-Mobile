using SurvivalShooter.Pooling;
using UnityEngine;

namespace SurvivalShooter.Audio
{
    public class PoolableAudioSource : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioSource _audioSource;
        public AudioSource AudioSource => _audioSource;
        
        public PoolKey PoolKey { get; set; }

        public void Initialize()
        {

        }

        public void Terminate()
        {
            _audioSource.clip = null;
        }
    }
}