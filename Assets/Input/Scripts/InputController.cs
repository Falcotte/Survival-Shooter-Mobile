using System;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Inputs
{
    public abstract class InputController : MonoBehaviour, IInputController
    {
        [SerializeField] private InputControllerType _controllerType;
        public InputControllerType ControllerType => _controllerType;

        public Vector2 Input { get; protected set; }
        public float Horizontal { get { return Input.x; } }
        public float HorizontalRaw { get { return Horizontal > 0 ? 1 : Horizontal < 0 ? -1 : 0; } }
        public float Vertical { get { return Input.y; } }
        public float VerticalRaw { get { return Vertical > 0 ? 1 : Vertical < 0 ? -1 : 0; } }
        public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

        public Action OnInputStart { set; get; }
        public Action OnInputStop { set; get; }

        private IInputService _inputService;

        protected virtual void Awake()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            _inputService = ServiceLocator.Get<IInputService>();

            Register();
        }

        public void Register()
        {
            _inputService.RegisterController(this);
        }

        public void Deregister()
        {
            _inputService.DeregisterController(this);
        }
    }
}