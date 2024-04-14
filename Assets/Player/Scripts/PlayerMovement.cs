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

            Turn(_movementDirection);
            _playerController.Animator.SetBool("IsWalking", _isMoving);
        }

        private void FixedUpdate()
        {
            Move(_inputController.Direction);
        }

        private void Move(Vector2 input)
        {
            _rigidbody.MovePosition(_rigidbody.position + new Vector3(input.x, 0f, input.y).normalized * _moveSpeed * Time.fixedDeltaTime);
        }

        private void Turn(Vector2 direction)
        {
            if(direction.sqrMagnitude > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(direction.x, 0f, direction.y).normalized, _rotateSpeed * Time.deltaTime, 0f);
                transform.LookAt(transform.position + newDirection);
            }
        }
    }
}