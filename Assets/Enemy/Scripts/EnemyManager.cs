using System.Collections.Generic;
using SurvivalShooter.Game;
using SurvivalShooter.Pooling;
using SurvivalShooter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SurvivalShooter.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        public Transform PlayerTransform => _playerTransform;
        
        [SerializeField] private List<Transform> _spawnPoints = new();

        [SerializeField] private float _spawnFrequency;
        private float _spawnTimer;

        private bool _isSpawning;

        private IGameService _gameService;
        private IPoolService _poolService;

        private void Start()
        {
            _gameService = ServiceLocator.Get<IGameService>();
            _poolService = ServiceLocator.Get<IPoolService>();
            
            _gameService.OnGameStart += EnableSpawning;
            _gameService.OnGameLose += DisableSpawning;
        }

        private void OnDisable()
        {
            _gameService.OnGameStart -= EnableSpawning;
            _gameService.OnGameLose -= DisableSpawning;
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (_isSpawning && _spawnTimer >= _spawnFrequency)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            List<PoolKey> enemyPoolKeys = new(){
                PoolKey.Zombunny,
                PoolKey.Zombear,
                PoolKey.Hellephant,
            };

            var enemyController = _poolService.EnemyPool.Get(enemyPoolKeys[Random.Range(0, 3)], transform);

            int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);

            enemyController.transform.position = _spawnPoints[randomSpawnPointIndex].position;
            enemyController.transform.rotation = _spawnPoints[randomSpawnPointIndex].rotation;

            enemyController.PlayerTransform = _playerTransform;
        }

        private void EnableSpawning()
        {
            _spawnTimer = 0f;

            _isSpawning = true;
        }

        private void DisableSpawning()
        {
            _isSpawning = false;
        }
    }
}