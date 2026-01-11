using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Buttons
{
    [RequireComponent(typeof(Button))]

    public class ReverseColumnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _includeColor;

        private bool _isInclude;

        private void Awake()
        {
            if (_button == null)
            {
                throw new NullReferenceException(
                    "Button reference is missing in MuteButton.");
            }
        }

        public void ToggleMuteState()
        {
            _isInclude = !_isInclude;

            UpdateButtonAppearance();
        }

        private void UpdateButtonAppearance()
        {
            _button.image.color = _isInclude ?
                _includeColor : _defaultColor;
        }
    }
}