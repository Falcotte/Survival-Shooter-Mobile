using SurvivalShooter.Services;

namespace SurvivalShooter.Pooling
{
    public interface IPoolService : IService
    {
        public AudioSourcePool AudioSourcePool { get; }
        
        public ParticleSystemPool ParticleSystemPool { get; }
        
        public EnemyPool EnemyPool { get; }
    }
}