using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalShooter.Inputs.Mobile
{
    public class Touchpad : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _backgroundBorders;
        [SerializeField] private RectTransform _handle;

        public Vector2 Position => _container.anchoredPosition;
        public float Radius { get; private set; }

        [SerializeField] private Color _activeColor;
        public Color ActiveColor => _activeColor;
        [SerializeField] private Color _inactiveColor;
        public Color InactiveColor => _inactiveColor;
        [SerializeField] private float _colorChangeDelay = 0.1f;
        [SerializeField] private float _colorChangeSpeed = 0.2f;

        private List<Image> _visuals = new();

        private IInputController _controller;

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            Register();

            Radius = _backgroundBorders.rect.size.x;

            SetVisuals();
        }

        public void Register()
        {
            _controller = GetComponentInParent<IInputController>();

            Debug.Log($"Touchpad registered to Controller - {_controller.ControllerType}");
        }

        private void SetVisuals()
        {
            foreach(var image in GetComponentsInChildren<Image>())
            {
                _visuals.Add(image);
            }
        }

        public void SetPosition(Vector2 position)
        {
            _container.anchoredPosition = position;
        }

        public void AdjustPosition(Vector2 position)
        {
            _container.anchoredPosition += position;
        }

        public void SetHandlePosition(Vector2 position)
        {
            _handle.transform.localPosition = position;
        }

        public void SetColor(Color color, float colorChangeDelayOverride = -1f, bool changeColorInstantly = false)
        {
            foreach(Image image in _visuals)
            {
                DOTween.Kill(image);

                if(!changeColorInstantly)
                {
                    image.DOColor(color, _colorChangeSpeed)
                        .SetEase(Ease.Linear)
                        .SetDelay(colorChangeDelayOverride < 0f ? _colorChangeDelay : colorChangeDelayOverride);
                }
                else
                {
                    image.color = color;
                }
            }
        }
    }
}