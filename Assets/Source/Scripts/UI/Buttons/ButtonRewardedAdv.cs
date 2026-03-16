using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Buttons
{
    public class ButtonRewardedAdv : StatefulButton
    {
        private const float DisableAlpha = 0.25f;
        private const float EnableAlpha = 1f;

        [SerializeField] private Image _image;

        private void Start()
        {
            Disable();
        }

        public void Disable()
        {
            Button.interactable = false;
            SetImageAlpha(DisableAlpha);

            SetState(true);
        }

        public void Enable()
        {
            Button.interactable = true;
            SetImageAlpha(EnableAlpha);

            ResetState();
        }

        //упростить реализацию
        private void SetImageAlpha(float alpha)
        {
            Color color = _image.color;
            color.a = alpha;
            _image.color = color;
        }
    }
}
