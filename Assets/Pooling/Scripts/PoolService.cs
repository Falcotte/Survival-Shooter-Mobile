using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Pooling
{
    public class PoolService : BaseService<IPoolService>, IPoolService
    {
        [SerializeField] private AudioSourcePool _audioSourcePool;
        public AudioSourcePool AudioSourcePool => _audioSourcePool;
        
        [SerializeField] private ParticleSystemPool _particleSystemPool;
        public ParticleSystemPool ParticleSystemPool => _particleSystemPool;
        
        [SerializeField] private EnemyPool _enemyPool;
        public EnemyPool EnemyPool => _enemyPool;
    }
}