using SurvivalShooter.Player;
using UnityEngine;

namespace SurvivalShooter.UI
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private SliderController _healthSlider;

        private void OnEnable()
        {
            PlayerHealth.OnTakeDamage += UpdateHealthSlider;
        }

        private void OnDisable()
        {
            PlayerHealth.OnTakeDamage -= UpdateHealthSlider;
        }

        private void UpdateHealthSlider(float currentHealthPercentage)
        {
            _healthSlider.SetValue(currentHealthPercentage);
        }
    }
}