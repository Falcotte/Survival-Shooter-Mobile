using System;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private int _startingHealth = 100;
        private int _currentHealth;
        public int CurrentHealth => _currentHealth;

        private bool _isDead;

        public static Action<float> OnTakeDamage;
        public static Action OnPlayerDeath;

        private void Start()
        {
            _currentHealth = _startingHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Max(0, _currentHealth);

            OnTakeDamage?.Invoke((float)_currentHealth/_startingHealth);

            if(_currentHealth <= 0 && !_isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            OnPlayerDeath?.Invoke();

            _playerController.Animator.SetTrigger("Die");

            _playerController.PlayerMovement.enabled = false;
            _playerController.PlayerShooting.enabled = false;
        }
    }
}