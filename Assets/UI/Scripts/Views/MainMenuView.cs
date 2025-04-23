using DG.Tweening;
using SurvivalShooter.Game;
using SurvivalShooter.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivalShooter.UI
{
    public class MainMenuView : BaseView, IPointerDownHandler
    {
        [SerializeField] private Transform _tapToPlayText;

        private IGameService _gameService;
        
        private void Start()
        {
            _gameService  = ServiceLocator.Get<IGameService>(); 
        }

        public override void Show()
        {
            base.Show();

            DOTween.Kill(_tapToPlayText);
            
            _tapToPlayText.transform.localScale = Vector3.one;
            _tapToPlayText.DOScale(1.2f, 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameService.StartGame();
        }
    }
}