using UnityEngine;
using UnityEngine.AI;

namespace SurvivalShooter.Enemy
{
    public class EnemyController : MonoBehaviour
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
    }
}