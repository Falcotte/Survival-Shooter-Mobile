using DG.Tweening;
using SurvivalShooter.Game;
using UnityEngine;

namespace SurvivalShooter.UI
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] protected GameState _gameState;
        public GameState GameState => _gameState;

        [SerializeField] protected CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup;

        [SerializeField] protected float _showDuration;
        public float ShowDuration => _showDuration;
        [SerializeField] protected float _hideDuration;
        public float HideDuration => _hideDuration;

        protected virtual void Awake()
        {
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            Sequence showSequence = DOTween.Sequence();
            showSequence.AppendCallback(() => { gameObject.SetActive(true); });
            showSequence.Append(_canvasGroup.DOFade(1f, _showDuration));
        }

        public virtual void Hide()
        {
            Sequence hideSequence = DOTween.Sequence();
            hideSequence.Append(_canvasGroup.DOFade(0f, _hideDuration));
            hideSequence.AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}