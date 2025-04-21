using SurvivalShooter.Player;
using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenAttacks = 0.5f;
        [SerializeField] private int _attackDamage = 10;

        [SerializeField] private EnemyController _enemyController;

        private PlayerController _playerController;

        private bool _playerInRange;
        private float _timer;

        private void Start()
        {
            _timer = _timeBetweenAttacks;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerController playerController))
            {
                _playerController = playerController;
                _playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<PlayerController>() != null)
            {
                _playerInRange = false;
            }
        }

        void Update()
        {
            _timer += Time.deltaTime;

            if(_timer >= _timeBetweenAttacks && _playerInRange && _enemyController.EnemyHealth.CurrentHealth > 0)
            {
                Attack();
            }
        }


        private void Attack()
        {
            _timer = 0f;

            if(_playerController != null && _playerController.PlayerHealth.CurrentHealth > 0)
            {
                _playerController.PlayerHealth.TakeDamage(_attackDamage);
            }
        }
    }
}