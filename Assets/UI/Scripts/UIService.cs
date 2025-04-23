using System.Collections.Generic;
using DG.Tweening;
using SurvivalShooter.Game;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.UI
{
    public class UIService : BaseService<IUIService>, IUIService
    {
        [SerializeField] private List<BaseView> _views = new();
        
        private BaseView _currentView;
        
        [SerializeField] private Camera _uICamera;
        public Camera UICamera => _uICamera;

        [SerializeField] private Canvas _canvas;
        public Canvas Canvas => _canvas;

        private IGameService _gameService;

        private void Start()
        {
            _gameService = ServiceLocator.Get<IGameService>();

            _gameService.OnGameStateChange += ChangeView;
            
            BaseView initialView = _views.Find(x => x.GameState == GameState.MainMenu);
            
            _currentView = initialView;
            _currentView.Show();
        }

        private void OnDisable()
        {
            _gameService.OnGameStateChange -= ChangeView;
        }

        private void ChangeView(GameState gameState)
        {
            Sequence changeViewSequence = DOTween.Sequence();
            
            float hideDuration = 0f;
            
            if(_currentView != null)
            {
                changeViewSequence.AppendCallback(() =>
                {
                    _currentView.Hide();
                });
                hideDuration = _currentView.HideDuration;
            }

            changeViewSequence.AppendInterval(hideDuration);

            changeViewSequence.AppendCallback(() =>
            {
                BaseView newView = _views.Find(x => x.GameState == gameState);
                
                _currentView = newView;
                _currentView.Show();
            });
        }
    }
}