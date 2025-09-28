using UnityEngine;
using YG;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CanvasMobileSetter _mobileSetter;
    [SerializeField] private CanvasPCSetter _PCSetter; // поменять название
    [SerializeField] private LanguageSetter _languageSetter;

    private void Awake()
    {
        _mobileSetter.Disable();
        _PCSetter.Disable();

        if (YG2.envir.isMobile)
        {
            _mobileSetter.Enable();
        }
        else
        {
            _PCSetter.Enable();
        }
    }
}
