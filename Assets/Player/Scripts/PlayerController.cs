using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private PlayerMovement _playerMovement;
        public PlayerMovement PlayerMovement => _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        public PlayerHealth PlayerHealth => _playerHealth;
    }
}