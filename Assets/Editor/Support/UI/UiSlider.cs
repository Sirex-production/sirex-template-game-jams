using EditorExtensions;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Support.UI
{
    /// <summary>
    /// Class that represents UI slider
    /// </summary>
    public class UiSlider : MonoBehaviour
    {
        [NotNull]
        [Tooltip("Image that represents front part of the slider")]
        [SerializeField] private Image frontImage;
        [Tooltip("Image that represents back part of the slider")]
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

        /// <summary>
        /// Sets front image fill amount between two values using linear interpolation
        /// </summary>
        /// <param name="value">Value that will be used as fill amount value after linear interpolation</param>
        /// <param name="minValue">Minimum value that will be used in linear interpolation</param>
        /// <param name="maxValue">Maximum value that will be used in linear interpolation</param>
        public void SetFrontImageFillAmount(float value, float minValue = 0, float maxValue = 1)
        {
            var actualFillAmount = Mathf.InverseLerp(minValue, maxValue, value);
            
            frontImageFillAmount = actualFillAmount;
            frontImage.fillAmount = actualFillAmount;
        }

        /// <summary>
        /// Sets back image fill amount between two values using linear interpolation
        /// </summary>
        /// <param name="value">Value that will be used as fill amount value after linear interpolation</param>
        /// <param name="minValue">Minimum value that will be used in linear interpolation</param>
        /// <param name="maxValue">Maximum value that will be used in linear interpolation</param>
        public void SetBackImageFillAmount(float value, float minValue = 0, float maxValue = 1)
        {
            var actualFillAmount = Mathf.InverseLerp(minValue, maxValue, value);

            backImageFillAmount = actualFillAmount;
            backImage.fillAmount = actualFillAmount;
        }

        /// <summary>
        /// Sets front image value with animation
        /// </summary>
        /// <param name="speed">Speed of animation</param>
        /// <param name="targetValue">Value that will be set in the end (should be between 0 and 1)</param>
        public void SetFrontImageWithLerping(float speed, float targetValue)
        {
            if(_frontImageCoroutine != null)
                StopCoroutine(_frontImageCoroutine);
            
            _frontImageCoroutine = this.LerpCoroutine(speed, FrontImageFillAmount, targetValue, f => SetFrontImageFillAmount(f));
        }
        
        /// <summary>
        /// Sets back image value with animation
        /// </summary>
        /// <param name="speed">Speed of animation</param>
        /// <param name="targetValue">Value that will be set in the end (should be between 0 and 1)</param>
        public void SetBackImageWithLerping(float speed, float targetValue)
        {
            if(_backImageCoroutine != null)
                StopCoroutine(_backImageCoroutine);
            
            _backImageCoroutine = this.LerpCoroutine(speed, BackImageFillAmount, targetValue, f => SetBackImageFillAmount(f));
        }
    }
}