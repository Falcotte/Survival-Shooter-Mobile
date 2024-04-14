using System.Collections.Generic;
using UnityEngine;

namespace SurvivalShooter.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        public Transform PlayerTransform => _playerTransform;

        [SerializeField] private List<EnemyController> _enemyControllerPrefabs = new();
        [SerializeField] private List<Transform> _spawnPoints = new();

        [SerializeField] private float _spawnFrequency;
        private float _spawnTimer;

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if(_spawnTimer >= _spawnFrequency)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }

        public void SpawnEnemy()
        {
            var enemyController = Instantiate(_enemyControllerPrefabs[Random.Range(0, _enemyControllerPrefabs.Count)], transform);
            enemyController.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

            enemyController.PlayerTransform = _playerTransform;
        }
    }
}