using UnityEngine;
using UnityEngine.UI;
//using Zenject;

[RequireComponent(typeof(Button))]

public abstract class BaseMenuButton : MonoBehaviour
{
    [SerializeField] protected GameHandler GameHandler;
    [SerializeField] protected Panel TargetPanel;

    protected Panel CurrentPanel;
    protected Button Button;
    //protected GameHandler GameHandler;

    //[Inject]
    //public void InjectDependencies(GameHandler gameHandler)
    //{
    //    GameHandler = gameHandler;

    //    Debug.Log(GameHandler);
    //}

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

    //нужно ли оставлять методы и переменные протектед?
    protected abstract void OnButtonClick();
}
