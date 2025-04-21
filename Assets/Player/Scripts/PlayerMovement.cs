using SurvivalShooter.Inputs;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private Vector3 _movementDirection;
        private bool _isMoving;

        private Transform _closestEnemy;

        private IInputService _inputService;
        private IInputController _inputController;

        private void Start()
        {
            _inputService = ServiceLocator.Get<IInputService>();
            _inputController = _inputService.GetController(InputControllerType.Mobile);
        }

        private void Update()
        {
            _isMoving = _inputController.Direction.sqrMagnitude > 0;

            if(_isMoving)
            {
                _movementDirection = _inputController.Direction;
            }
            else
            {
                _movementDirection = Vector2.zero;
            }

            _playerController.Animator.SetBool("IsWalking", _isMoving);
        }

        private void FixedUpdate()
        {
            Move(_inputController.Direction);

            _closestEnemy = _playerController.PlayerRange.GetClosestEnemy()?.transform;

            Turn(_closestEnemy == null ? _movementDirection : 
                new Vector2(_closestEnemy.position.x - _playerController.PlayerShooting.GunPivot.position.x,
                _closestEnemy.position.z - _playerController.PlayerShooting.GunPivot.position.z));
        }

        private void Move(Vector2 input)
        {
            _rigidbody.MovePosition(_rigidbody.position + new Vector3(input.x, 0f, input.y).normalized * _moveSpeed * Time.fixedDeltaTime);
        }

        private void Turn(Vector2 direction)
        {
            if(direction.sqrMagnitude > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(direction.x, 0f, direction.y).normalized, _rotateSpeed * Time.fixedDeltaTime, 0f);
                transform.LookAt(transform.position + newDirection);
            }
        }
    }
}