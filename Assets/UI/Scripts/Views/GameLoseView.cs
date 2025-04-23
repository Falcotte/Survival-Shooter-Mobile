using DG.Tweening;
using SurvivalShooter.Game;
using SurvivalShooter.Services;

namespace SurvivalShooter.UI
{
    public class GameLoseView : BaseView
    {
        private IGameService _gameService;

        private void Start()
        {
            _gameService = ServiceLocator.Get<IGameService>();
        }

        public override void Show()
        {
            base.Show();

            Sequence restartSequence = DOTween.Sequence();
            restartSequence.AppendInterval(1f);
            restartSequence.AppendCallback(() =>
            {
                _gameService.GoToMainMenuState();
            });
        }
    }
}