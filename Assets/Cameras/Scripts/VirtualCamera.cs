using Cinemachine;
using SurvivalShooter.Game;
using UnityEngine;

namespace SurvivalShooter.Cameras
{
    public class VirtualCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        public CinemachineVirtualCamera Camera => _camera;

        [SerializeField] private bool _isBoundToGameState;
        public bool IsBoundToGameState => _isBoundToGameState;

        [SerializeField] private GameState _gameState;
        public GameState GameState => _gameState;
        
        [SerializeField] private float _blendTime;
        public float BlendTime => _blendTime;
        
        [SerializeField] private CinemachineBlendDefinition.Style _blendStyle;
        public CinemachineBlendDefinition.Style BlendStyle => _blendStyle;
    }
}