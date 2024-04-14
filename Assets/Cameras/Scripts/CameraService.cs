using SurvivalShooter.Services;
using SurvivalShooter.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SurvivalShooter.Cameras
{
    public class CameraService : BaseService<ICameraService>, ICameraService
    {
        [SerializeField] private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        private UniversalAdditionalCameraData _cameraData;

        private IUIService _uIService;

        private void Start()
        {
            _uIService = ServiceLocator.Get<IUIService>();

            SetCameraStack();
        }

        private void SetCameraStack()
        {
            _cameraData = _mainCamera.GetUniversalAdditionalCameraData();
            _cameraData.renderType = CameraRenderType.Base;

            var uICameraData = _uIService.UICamera.GetUniversalAdditionalCameraData();
            uICameraData.renderType = CameraRenderType.Overlay;

            _cameraData.cameraStack.Add(_uIService.UICamera);
        }
    }
}