using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        public EnemyMovement EnemyMovement => _enemyMovement;

        public Transform PlayerTransform { get; set; } 
    }
}