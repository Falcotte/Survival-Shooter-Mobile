using SurvivalShooter.Player;
using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;
        
        private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");

        private bool _isPlayerDead;

        private void OnEnable()
        {
            PlayerHealth.OnPlayerDeath += DisableEnemyMovement;
        }

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDeath -= DisableEnemyMovement;
        }

        private void Update()
        {
            if(_enemyController.EnemyHealth.CurrentHealth > 0 && !_isPlayerDead)
            {
                _enemyController.NavMeshAgent.SetDestination(_enemyController.PlayerTransform.position);
            }
        }

        private void DisableEnemyMovement()
        {
            _isPlayerDead = true;
            _enemyController.NavMeshAgent.enabled = false;
            
            _enemyController.Animator.SetTrigger(PlayerDead);
        }
    }
}