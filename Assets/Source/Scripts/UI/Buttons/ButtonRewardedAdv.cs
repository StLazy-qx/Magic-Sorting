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

        private void SetImageAlpha(float alpha)
        {
            _image.color = new Color(
                _image.color.r, 
                _image.color.g, 
                _image.color.b, 
                alpha);
        }
    }
}
