//[RequireComponent(typeof(Button))]

public class PanelSwitchButton : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        CurrentPanel?.Close();
        TargetPanel?.Open();
    }

    //[SerializeField] private Panel _targetPanel;
    //[SerializeField] private Panel _currentPanel;

    //private Button _button;

    ////меню- язык,сложность, магазин, звук
    ////кнопки назад и сокрыть на эскейп обратно в игру любую панель на дефолт
    ////

    //private void Awake()
    //{
    //    _button = GetComponent<Button>();
    //}

    //private void OnEnable()
    //{
    //    _button.onClick.AddListener(OnButtonClick);
    //}

    //private void OnDisable()
    //{
    //    _button.onClick.RemoveListener(OnButtonClick);
    //}

    //private void OnButtonClick()
    //{
    //    if (_currentPanel != null)
    //    {
    //        _currentPanel.Close();
    //    }

    //    if (_targetPanel != null)
    //    {
    //        _targetPanel.Open();
    //    }
    //}
}
