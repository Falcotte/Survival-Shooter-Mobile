using SurvivalShooter.Game;
using SurvivalShooter.Pooling;
using SurvivalShooter.Services;
using UnityEngine;
using UnityEngine.AI;

namespace SurvivalShooter.Enemy
{
    public class EnemyController : MonoBehaviour, IPoolable
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        [SerializeField] private EnemyMovement _enemyMovement;
        public EnemyMovement EnemyMovement => _enemyMovement;
        [SerializeField] private EnemyHealth _enemyHealth;
        public EnemyHealth EnemyHealth => _enemyHealth;

        public Transform PlayerTransform { get; set; }
        public PoolKey PoolKey { get; set; }

        private IGameService _gameService;
        private IPoolService _poolService;
        
        private void Awake()
        {
            _gameService = ServiceLocator.Get<IGameService>();
            _poolService = ServiceLocator.Get<IPoolService>();
        }
        
        public void Initialize()
        {
            _enemyMovement.EnableEnemyMovement();
            _enemyHealth.ResetEnemyHealth();
            
            _gameService.OnGameReset += ReturnToPool;
        }

        public void Terminate()
        {
            _gameService.OnGameReset -= ReturnToPool;
        }

        private void ReturnToPool()
        {
            _poolService.EnemyPool.Return(this);
        }
    }
}