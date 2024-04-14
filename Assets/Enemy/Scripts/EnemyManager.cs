using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        public Transform PlayerTransform => _playerTransform;
    }
}