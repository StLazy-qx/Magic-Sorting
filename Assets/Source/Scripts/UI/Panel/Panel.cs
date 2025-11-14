using UnityEngine;

public class Panel : MonoBehaviour
{
    public void Close()
        => gameObject.SetActive(false);

    public void Open()
        => gameObject.SetActive(true);
}
