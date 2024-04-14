using System;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private int _startingHealth = 100;
        private int _currentHealth;

        private bool _isDead;

        public static Action<float> OnTakeDamage;

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

            _playerController.Animator.SetTrigger("Die");

            _playerController.PlayerMovement.enabled = false;
        }
    }
}