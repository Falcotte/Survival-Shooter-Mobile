using System;
using SurvivalShooter.Audio;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private AudioKey _hurtAudioKey;
        [SerializeField] private AudioKey _deathAudioKey;
        
        [SerializeField] private int _startingHealth = 100;
        private int _currentHealth;
        public int CurrentHealth => _currentHealth;

        private IAudioService _audioService;
        
        private bool _isDead;

        public static Action<float> OnTakeDamage;
        public static Action OnPlayerDeath;

        private void Start()
        {
            _audioService = ServiceLocator.Get<IAudioService>();
            
            _currentHealth = _startingHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Max(0, _currentHealth);

            _audioService.PlayAudio(_hurtAudioKey);
            
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

            _audioService.PlayAudio(_deathAudioKey);
            
            _playerController.Animator.SetTrigger("Die");

            _playerController.PlayerMovement.enabled = false;
            _playerController.PlayerShooting.enabled = false;
        }
    }
}