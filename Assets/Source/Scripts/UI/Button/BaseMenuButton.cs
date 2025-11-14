using UnityEngine;
using UnityEngine.UI;
using GameBehaviour;

[RequireComponent(typeof(Button))]

public abstract class BaseMenuButton : MonoBehaviour
{
    [SerializeField] protected GameHandler GameHandler;
    [SerializeField] protected Panel TargetPanel;

    protected Panel CurrentPanel;
    protected Button Button;

    private void Awake()
    {
        CurrentPanel = GetComponentInParent<Panel>();
        Button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}