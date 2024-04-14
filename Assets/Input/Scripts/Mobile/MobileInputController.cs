using SurvivalShooter.Services;
using SurvivalShooter.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivalShooter.Inputs.Mobile
{
    public class MobileInputController : InputController, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Touchpad _touchpadPrefab;

        private RectTransform _rectTransform;
        private RectTransform _canvasRectTransform;

        private Vector2 _boundaryMin;
        private Vector2 _boundaryMax;
        private float _width;
        private float _height;

        private Vector2 _initialPosition;
        private Vector2 _currentPosition;

        private Touchpad _touchpad;

        private IUIService _uIService;

        protected override void Setup()
        {
            base.Setup();

            _uIService = ServiceLocator.Get<IUIService>();

            _rectTransform = GetComponent<RectTransform>();
            _canvasRectTransform = _uIService.Canvas.GetComponent<RectTransform>();

            _boundaryMin = new Vector2(_rectTransform.anchorMin.x * Screen.width, _rectTransform.anchorMin.y * Screen.height);
            _boundaryMax = new Vector2(_rectTransform.anchorMax.x * Screen.width, _rectTransform.anchorMax.y * Screen.height);

            _width = (_rectTransform.anchorMax.x - _rectTransform.anchorMin.x) * Screen.width;
            _height = (_rectTransform.anchorMax.y - _rectTransform.anchorMin.y) * Screen.height;

            _touchpad = Instantiate(_touchpadPrefab, transform);
            _touchpad.SetColor(_touchpad.InactiveColor, changeColorInstantly: true);
        }

        #region Touch

        public void OnPointerDown(PointerEventData eventData)
        {
            _initialPosition = AdjustEventDataPosition(eventData.position);
            _currentPosition = _initialPosition;

            _touchpad.SetPosition(_currentPosition);
            _touchpad.SetHandlePosition(Vector2.zero);
            _touchpad.SetColor(_touchpad.ActiveColor);

            OnInputStart?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnInputStop?.Invoke();

            _touchpad.SetColor(_touchpad.InactiveColor);

            _initialPosition = Vector2.zero;
            _currentPosition = Vector2.zero;

            Input = Vector2.zero;
        }

        #endregion

        #region Drag

        public void OnDrag(PointerEventData eventData)
        {
            _currentPosition = AdjustEventDataPosition(eventData.position);

            Vector2 input = _currentPosition - _initialPosition;
            float inputMagnitude = input.magnitude;

            if(inputMagnitude > _touchpad.Radius / 2f)
            {
                _touchpad.AdjustPosition(new Vector2(input.x, input.y).normalized * (inputMagnitude - _touchpad.Radius / 2f));
                _initialPosition = _touchpad.Position;

                Input = input.normalized;
            }
            else
            {
                Input = input / (_touchpad.Radius / 2f);
            }

            _touchpad.SetHandlePosition(Input * _touchpad.Radius / 2f);
        }

        #endregion

        private Vector2 AdjustEventDataPosition(Vector2 eventDataPosition)
        {
            return new Vector2(eventDataPosition.x / Screen.width * _canvasRectTransform.rect.width - (_width / 2f + _boundaryMin.x) / Screen.width * _canvasRectTransform.rect.width,
                eventDataPosition.y / Screen.height * _canvasRectTransform.rect.height - _boundaryMin.y / Screen.height * _canvasRectTransform.rect.height);
        }
    }
}