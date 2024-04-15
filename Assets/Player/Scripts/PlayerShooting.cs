using SurvivalShooter.Enemy;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _gunBarrelEnd;
        public Transform GunBarrelEnd => _gunBarrelEnd;

        [SerializeField] private int _damagePerShot = 20;
        [SerializeField] private float _timeBetweenBullets = 0.15f;
        [SerializeField] private float _range = 100f;

        [SerializeField] private ParticleSystem _gunParticles;
        [SerializeField] private LineRenderer _gunLine;
        [SerializeField] private Light _gunLight;
        [SerializeField] private Light _faceLight;

        private Ray _shootRay = new();
        private RaycastHit _shootHit;
        private int _shootableMask;

        private float _timer;

        float _effectsDisplayTime = 0.2f;

        private Transform _closestEnemy;

        private void Start()
        {
            _shootableMask = LayerMask.GetMask("Shootable");

            _timer = _timeBetweenBullets;
        }

        private void Update()
        {
            _closestEnemy = _playerController.PlayerRange.GetClosestEnemy()?.transform;

            _timer += Time.deltaTime;

            if(_closestEnemy != null && _timer >= _timeBetweenBullets)
            {
                AttemptToShoot();
            }

            if(_timer >= _timeBetweenBullets * _effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        private void AttemptToShoot()
        {
            _shootRay.origin = _gunBarrelEnd.position;
            _shootRay.direction = _gunBarrelEnd.forward;

            if(Physics.Raycast(_shootRay, out _shootHit, _range, _shootableMask))
            {
                if(_shootHit.collider.GetComponent<EnemyController>() != null)
                {
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            _timer = 0f;

            _gunLight.enabled = true;
            _faceLight.enabled = true;

            _gunParticles.Stop();
            _gunParticles.Play();

            _gunLine.enabled = true;
            _gunLine.SetPosition(0, _gunBarrelEnd.position);

            if(Physics.Raycast(_shootRay, out _shootHit, _range, _shootableMask))
            {
                if(_shootHit.collider.TryGetComponent(out EnemyController enemyController))
                {
                    enemyController.EnemyHealth.TakeDamage(_damagePerShot, _shootHit.point);
                }

                _gunLine.SetPosition(1, _shootHit.point);
            }
            else
            {
                _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * _range);
            }
        }

        private void DisableEffects()
        {
            _gunLine.enabled = false;

            _faceLight.enabled = false;
            _gunLight.enabled = false;
        }
    }
}