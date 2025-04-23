using System;
using System.Collections.Generic;
using SurvivalShooter.Game;
using SurvivalShooter.Player;
using SurvivalShooter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private bool _isSpawning;

        private IGameService _gameService;

        private void Start()
        {
            _gameService = ServiceLocator.Get<IGameService>();

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

            if(_isSpawning && _spawnTimer >= _spawnFrequency)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            var enemyController = Instantiate(_enemyControllerPrefabs[Random.Range(0, _enemyControllerPrefabs.Count)], transform);

            int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);

            enemyController.transform.position = _spawnPoints[randomSpawnPointIndex].position;
            enemyController.transform.rotation = _spawnPoints[randomSpawnPointIndex].rotation;

            enemyController.PlayerTransform = _playerTransform;
        }

        private void EnableSpawning()
        {
            _isSpawning = true;
        }

        private void DisableSpawning()
        {
            _isSpawning = false;
        }
    }
}