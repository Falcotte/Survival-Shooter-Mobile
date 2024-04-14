using UnityEngine;
using UnityEngine.AI;

namespace SurvivalShooter.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyController;

        [SerializeField] private NavMeshAgent _navMeshAgent;

        private void Update()
        {
            _navMeshAgent.SetDestination(_enemyController.PlayerTransform.position);
        }
    }
}