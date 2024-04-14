using UnityEngine;
using UnityEngine.UI;

namespace SurvivalShooter.UI
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private Image _sliderMaskImage;

        private void Start()
        {
            _sliderMaskImage.fillAmount = 1f;
        }

        public void SetValue(float amount)
        {
            _sliderMaskImage.fillAmount = amount;
        }
    }
}