using System.Collections.Generic;
using SurvivalShooter.Services;
using SurvivalShooter.UI;
using UnityEngine;

namespace SurvivalShooter.Inputs
{
    public class InputService : BaseService<IInputService>, IInputService
    {
        [SerializeField] private InputController _mobileControllerPrefab;

        private Dictionary<InputControllerType, IInputController> _controllers = new();

        private IUIService _uIService;

        private void Start()
        {
            _uIService = ServiceLocator.Get<IUIService>();

            InstantiateController(_mobileControllerPrefab);
        }

        private void InstantiateController(InputController controller)
        {
            Instantiate(controller, _uIService.Canvas.transform);
        }

        public void RegisterController(IInputController controller)
        {
            if(_controllers.ContainsKey(controller.ControllerType))
            {
                Debug.LogWarning($"Controller - {controller.ControllerType} already registered");
            }

            _controllers.Add(controller.ControllerType, controller);
            Debug.Log($"Controller - {controller.ControllerType} registered");
        }

        public void DeregisterController(IInputController controller)
        {
            if(_controllers.ContainsKey(controller.ControllerType))
            {
                Debug.LogWarning($"Controller - {controller.ControllerType} not registered");
            }

            _controllers.Remove(controller.ControllerType);
            Debug.Log($"Controller - {controller.ControllerType} deregistered");
        }

        public IInputController GetController(InputControllerType controllerType)
        {
            return _controllers[controllerType];
        }
    }
}