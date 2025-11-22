using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _includeColor;

    private bool _isInclude;

    public Button.ButtonClickedEvent OnClick => _button.onClick;

    public void SetMuteState(bool isMuted)
    {
        _isInclude = isMuted;
        UpdateButtonAppearance();
    }

    public void ToggleMuteState()
    {
        _isInclude = !_isInclude;
        UpdateButtonAppearance();
    }

    private void UpdateButtonAppearance()
    {
        _button.image.color = _isInclude ? _includeColor : _defaultColor;
    }
}
