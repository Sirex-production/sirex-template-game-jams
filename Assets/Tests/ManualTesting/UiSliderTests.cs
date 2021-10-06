using System.Collections;
using EditorExtensions;
using Support.UI;
using UnityEngine;

namespace Tests.Manual
{
    public class UiSliderTests : MonoBehaviour
    {
        [NotNull]
        [SerializeField] private UiSlider slider;

        private WaitForSeconds _pause = new WaitForSeconds(1f);

        private void Start()
        {
            if(slider != null)
                StartCoroutine(PlayIncreaseAnimation());
        }

        private IEnumerator PlayIncreaseAnimation()
        {
            yield return _pause;
            slider.SetFrontImageFillAmount(1);
            yield return _pause;
            slider.SetBackImageFillAmount(1);
            yield return _pause;
            slider.SetFrontImageFillAmount(.7f);
            yield return _pause;
            slider.SetBackImageFillAmount(.8f);
        }
    }
}