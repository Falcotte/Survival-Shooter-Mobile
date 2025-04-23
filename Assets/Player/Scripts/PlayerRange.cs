using System.Collections.Generic;
using SurvivalShooter.Enemy;
using SurvivalShooter.Game;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerRange : MonoBehaviour
    {
        private List<EnemyController> _enemiesInRange = new();

        private IGameService _gameService;
        
        private void Awake()
        {
            _gameService = ServiceLocator.Get<IGameService>();
        }
        
        private void OnEnable()
        {
            EnemyHealth.OnEnemyDeath += RemoveEnemyFromRange;
            
            _gameService.OnGameReset += _enemiesInRange.Clear;
        }

        private void OnDisable()
        {
            EnemyHealth.OnEnemyDeath -= RemoveEnemyFromRange;
            
            _gameService.OnGameReset -= _enemiesInRange.Clear;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out EnemyController enemyController))
            {
                if(enemyController.EnemyHealth.CurrentHealth > 0)
                {
                    _enemiesInRange.Add(enemyController);
                }
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