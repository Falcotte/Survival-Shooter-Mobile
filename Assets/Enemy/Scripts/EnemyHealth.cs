using System;
using SurvivalShooter.Audio;
using SurvivalShooter.Particles;
using SurvivalShooter.Pooling;
using SurvivalShooter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SurvivalShooter.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;

        [SerializeField] private Vector3 _bodyCenterPosition;
        [SerializeField] private Vector2 _hitParticlesRotationOffset;

        [SerializeField] private AudioKey _hurtAudioKey;
        [SerializeField] private AudioKey _deathAudioKey;

        [SerializeField] private PoolKey _hitParticlesPoolKey;
        [SerializeField] private PoolKey _deathParticlesPoolKey;

        [SerializeField] private int _startingHealth = 100;
        private int _currentHealth;
        public int CurrentHealth => _currentHealth;
        [SerializeField] private float _sinkSpeed = 2.5f;

        [SerializeField] private int _scoreValue = 10;
        
        private IAudioService _audioService;
        private IPoolService _poolService;

        private ParticleSystemPool _particleSystemPool;

        private bool _isDead;
        private bool _isSinking;

        public static Action<EnemyController> OnEnemyDeath;

        private void Start()
        {
            _audioService = ServiceLocator.Get<IAudioService>();
            _poolService = ServiceLocator.Get<IPoolService>();

            _particleSystemPool = _poolService.ParticleSystemPool;
            
            _currentHealth = _startingHealth;
        }

        private void Update()
        {
            if (_isSinking)
            {
                transform.Translate(-Vector3.up * _sinkSpeed * Time.deltaTime);
            }
        }

        public void ResetEnemyHealth()
        {
            _currentHealth = _startingHealth;

            _isDead = false;
            _isSinking = false;
            
            transform.localPosition = Vector3.zero;
            
            _capsuleCollider.enabled = true;
            
            _enemyController.NavMeshAgent.enabled = true;
            _rigidbody.isKinematic = false;
            
            _enemyController.Animator.Rebind();
            _enemyController.Animator.Update(0f);
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (_isDead)
                return;

            _currentHealth -= amount;

            PoolableParticleSystem hitParticles = _particleSystemPool.Get(_hitParticlesPoolKey);

            hitParticles.transform.position = hitPoint;

            Vector3 hitParticlesDirection = hitPoint - _bodyCenterPosition - transform.position;
            hitParticlesDirection.y = 0;

            hitParticles.transform.rotation = Quaternion.LookRotation(hitParticlesDirection) *
                                              Quaternion.Euler(0, Random.Range(_hitParticlesRotationOffset.x,
                                                  _hitParticlesRotationOffset.y), 0);

            hitParticles.OnStopped = (particleSystem) => _particleSystemPool.Return(particleSystem);
            hitParticles.ParticleSystem.Play();

            _audioService.PlayAudio(_hurtAudioKey);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            _isDead = true;
            OnEnemyDeath?.Invoke(_enemyController);

            _capsuleCollider.enabled = false;

            _enemyController.Animator.SetTrigger("Dead");

            PoolableParticleSystem deathParticles = _particleSystemPool.Get(_deathParticlesPoolKey);

            deathParticles.transform.position = transform.position + Vector3.up * 0.5f;
            deathParticles.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            deathParticles.OnStopped = (particleSystem) => _particleSystemPool.Return(particleSystem);
            deathParticles.ParticleSystem.Play();

            _audioService.PlayAudio(_deathAudioKey);
        }

        public void StartSinking()
        {
            _enemyController.NavMeshAgent.enabled = false;
            _rigidbody.isKinematic = true;

            _isSinking = true;

            _poolService.EnemyPool.Return(_enemyController, 2f);
        }
    }
}