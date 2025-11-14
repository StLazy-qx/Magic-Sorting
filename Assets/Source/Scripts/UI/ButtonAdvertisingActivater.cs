using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]

public class ButtonAdvertisingActivater : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(YG2.InterstitialAdvShow);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(YG2.InterstitialAdvShow);
    }
}
