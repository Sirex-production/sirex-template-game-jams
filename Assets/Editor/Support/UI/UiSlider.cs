using EditorExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace Support.UI
{
    public class UiSlider : MonoBehaviour
    {
        [NotNull]
        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [Space]
        [SerializeField] [Range(0, 1)] private float frontImageFillAmount = 1;
        [SerializeField] [Range(0, 1)] private float backImageFillAmount = 1;

        public float FrontImageFillAmount => frontImageFillAmount;
        public float BackImageFillAmount => backImageFillAmount;

        private Coroutine _frontImageCoroutine;
        private Coroutine _backImageCoroutine;

        private void OnValidate()
        {
            if (frontImage != null)
            {
                frontImage.type = Image.Type.Filled;
                frontImage.fillAmount = frontImageFillAmount;
            }

            if (backImage != null)
            {
                backImage.type = Image.Type.Filled;
                backImage.fillAmount = backImageFillAmount;
            }
        }

        public void SetFrontImageFillAmount(float value, float minValue = 0, float maxValue = 1)
        {
            var actualFillAmount = Mathf.InverseLerp(minValue, maxValue, value);
            
            frontImageFillAmount = actualFillAmount;
            frontImage.fillAmount = actualFillAmount;
        }

        public void SetBackImageFillAmount(float value, float minValue = 0, float maxValue = 1)
        {
            var actualFillAmount = Mathf.InverseLerp(minValue, maxValue, value);

            backImageFillAmount = actualFillAmount;
            backImage.fillAmount = actualFillAmount;
        }
    }
}