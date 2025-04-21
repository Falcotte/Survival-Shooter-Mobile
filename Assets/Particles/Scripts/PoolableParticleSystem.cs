using System;
using SurvivalShooter.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace SurvivalShooter.Particles
{
    public class PoolableParticleSystem : MonoBehaviour, IPoolable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public ParticleSystem ParticleSystem => _particleSystem;

        public UnityAction<PoolableParticleSystem> OnStopped;
        
        public PoolKey PoolKey { get; set; }

        public void Initialize()
        {

        }

        public void Terminate()
        {
            _particleSystem.Stop();
        }

        private void OnParticleSystemStopped()
        {
            OnStopped?.Invoke(this);
        }
    }
}