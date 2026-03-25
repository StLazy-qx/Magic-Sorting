using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Buttons
{
    public class StatefulButton : MonoBehaviour
    {
        [SerializeField] protected Button Button;
        [SerializeField] protected Color DefaultColor;
        [SerializeField] protected Color IncludeColor;

        protected bool IsActive;

        public event Action Activated;

        public Button.ButtonClickedEvent OnClick => Button.onClick;

        protected virtual void Awake()
        {
            if (Button == null)
            {
                throw new NullReferenceException(
                    "Button reference is missing in MuteButton.");
            }

            Button.onClick.AddListener(HandleClick);
            UpdateAppearance();
        }

        public void SetState(bool isActive)
        {
            IsActive = isActive;

            UpdateAppearance();

            if (isActive)
                Activated.Invoke();
        }

        public void ResetState()
        {
            SetState(false);
        }

        private void HandleClick()
        {
            SetState(IsActive == false);
        }

        private void UpdateAppearance()
        {
            Button.image.color = IsActive
                ? IncludeColor
                : DefaultColor;
        }
    }
}