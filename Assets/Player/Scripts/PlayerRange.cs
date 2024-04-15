using System.Collections.Generic;
using SurvivalShooter.Enemy;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerRange : MonoBehaviour
    {
        private List<EnemyController> _enemiesInRange = new();

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out EnemyController enemyController))
            {
                _enemiesInRange.Add(enemyController);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out EnemyController enemyController))
            {
                _enemiesInRange.Remove(enemyController);
            }
        }

        private void RemoveEnemyFromRange(EnemyController enemyController)
        {
            if(_enemiesInRange.Contains(enemyController))
            {
                _enemiesInRange.Remove(enemyController);
            }
        }

        public EnemyController GetClosestEnemy()
        {
            EnemyController closestEnemy = null;

            float minDistance = Mathf.Infinity;

            foreach(EnemyController enemy in _enemiesInRange)
            {
                float distance = Vector3.SqrMagnitude(enemy.transform.position - transform.position);
                if(distance < minDistance)
                {
                    closestEnemy = enemy;
                    minDistance = distance;
                }
            }

            return closestEnemy;
        }
    }
}