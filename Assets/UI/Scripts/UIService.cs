using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.UI
{
    public class UIService : BaseService<IUIService>, IUIService
    {
        [SerializeField] private Camera _uICamera;
        public Camera UICamera => _uICamera;

        [SerializeField] private Canvas _canvas;
        public Canvas Canvas => _canvas;
    }
}