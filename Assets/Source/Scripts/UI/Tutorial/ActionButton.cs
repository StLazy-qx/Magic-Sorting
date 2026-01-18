using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.UI.Tutorial
{
    [RequireComponent(typeof(Button))]

    class ActionButton : MonoBehaviour
    {
        private Button _button;

        public UnityEngine.Events.UnityEvent OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(
                () => OnClick?.Invoke());
        }
    }
}
