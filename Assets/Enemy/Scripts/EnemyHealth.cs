using System;
using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private ParticleSystem _deathParticles;

        [SerializeField] private int _startingHealth = 100;
        [SerializeField] private int _currentHealth;
        public int CurrentHealth => _currentHealth;
        [SerializeField] private float _sinkSpeed = 2.5f;

        [SerializeField] private int _scoreValue = 10;

        private bool _isDead;
        private bool _isSinking;

        public static Action<EnemyController> OnEnemyDeath;

        private void Start()
        {
            _currentHealth = _startingHealth;
        }

        private void Update()
        {
            if(_isSinking)
            {
                transform.Translate(-Vector3.up * _sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if(_isDead)
                return;

            _currentHealth -= amount;

            _hitParticles.transform.position = hitPoint;
            _hitParticles.Play();

            if(_currentHealth <= 0)
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
            _deathParticles.Play();
        }

        public void StartSinking()
        {
            _enemyController.NavMeshAgent.enabled = false;
            _rigidbody.isKinematic = true;

            _isSinking = true;

            Destroy(_enemyController.gameObject, 2f);
        }
    }
}