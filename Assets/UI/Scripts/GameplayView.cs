using DG.Tweening;
using SurvivalShooter.Player;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalShooter.UI
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private SliderController _healthSlider;
        [SerializeField] private Image _damageImage;

        [SerializeField] private float _damageImageAlphaAmount;
        [SerializeField] private float _damageImageAnimationDuration;

        private void OnEnable()
        {
            PlayerHealth.OnTakeDamage += UpdateHealthSlider;
            PlayerHealth.OnTakeDamage += ShowDamageImage;
        }

        private void OnDisable()
        {
            PlayerHealth.OnTakeDamage -= UpdateHealthSlider;
            PlayerHealth.OnTakeDamage -= ShowDamageImage;
        }

        private void UpdateHealthSlider(float currentHealthPercentage)
        {
            _healthSlider.SetValue(currentHealthPercentage);
        }

        private void ShowDamageImage(float currentHealthPercentage)
        {
            _damageImage.DOFade(_damageImageAlphaAmount, _damageImageAnimationDuration * .3f).OnComplete(() =>
            {
                _damageImage.DOFade(0f, _damageImageAnimationDuration * .7f);
            });
        }
    }
}