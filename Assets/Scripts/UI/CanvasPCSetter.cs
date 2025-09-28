using UnityEngine;

public class CanvasPCSetter : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
