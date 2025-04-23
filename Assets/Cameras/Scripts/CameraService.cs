using System.Collections.Generic;
using Cinemachine;
using SurvivalShooter.Game;
using SurvivalShooter.Services;
using SurvivalShooter.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SurvivalShooter.Cameras
{
    public class CameraService : BaseService<ICameraService>, ICameraService
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;

        [SerializeField] private List<VirtualCamera> _stateCameras;

        [SerializeField] private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        private UniversalAdditionalCameraData _cameraData;

        private VirtualCamera _previousVirtualCamera;
        private VirtualCamera _currentVirtualCamera;
        
        private IGameService _gameService;
        private IUIService _uIService;

        private void Start()
        {
            _gameService = ServiceLocator.Get<IGameService>();
            _uIService = ServiceLocator.Get<IUIService>();

            _gameService.OnGameStateChange += ChangeCamera;

            SetCameraStack();

            CloseAllCameras();

            OpenInitialCamera();
        }

        private void OnDisable()
        {
            _gameService.OnGameStateChange -= ChangeCamera;
        }

        private void SetCameraStack()
        {
            _cameraData = _mainCamera.GetUniversalAdditionalCameraData();
            _cameraData.renderType = CameraRenderType.Base;

            var uICameraData = _uIService.UICamera.GetUniversalAdditionalCameraData();
            uICameraData.renderType = CameraRenderType.Overlay;

            _cameraData.cameraStack.Add(_uIService.UICamera);
        }
        
        private void CloseAllCameras()
        {
            foreach(var camera in _stateCameras)
            {
                camera.Camera.Priority = 0;
            }
        }
        
        private void OpenInitialCamera()
        {
            if(_stateCameras.FindAll(x => x.GameState == GameState.MainMenu && x.IsBoundToGameState).Count > 1)
            {
                Debug.LogWarning($"Found more than 1 camera for state {GameState.MainMenu}");
            }

            VirtualCamera newCamera = _stateCameras.Find(x => x.GameState == GameState.MainMenu && x.IsBoundToGameState);

            _currentVirtualCamera = newCamera;
            
            _currentVirtualCamera.Camera.Priority = 1;
            _cinemachineBrain.m_DefaultBlend.m_Time = 0f;
        }

        public void ChangeCamera(GameState currentState)
        {
            if (_stateCameras.FindAll(x => x.GameState == currentState && x.IsBoundToGameState).Count > 1)
            {
                Debug.LogWarning($"Found more than 1 camera for state {currentState}");
            }

            VirtualCamera newCamera = _stateCameras.Find(x => x.GameState == currentState && x.IsBoundToGameState);

            if (_currentVirtualCamera != null)
            {
                _previousVirtualCamera = _currentVirtualCamera;
                _currentVirtualCamera.Camera.Priority = 0;
            }

            _currentVirtualCamera = newCamera;
            
            _currentVirtualCamera.Camera.Priority = 1;
            
            _cinemachineBrain.m_DefaultBlend.m_Time = _currentVirtualCamera.BlendTime;
            _cinemachineBrain.m_DefaultBlend.m_Style = _currentVirtualCamera.BlendStyle;

            Debug.Log($"Changing camera -> Game State [{currentState}] Blend Time [{_currentVirtualCamera.BlendTime}]");
        }
    }
}