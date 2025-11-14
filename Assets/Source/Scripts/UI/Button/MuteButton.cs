using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Color _includeColor;

    private Color _defaultColor;

    public Button.ButtonClickedEvent OnClick => _button.onClick;

    private void Awake()
    {
        _defaultColor = _button.image.color;
    }

    public void UpdateButtonColor(bool isMuted)
    {
        _button.image.color = isMuted ? _includeColor : _defaultColor;
    }
}
