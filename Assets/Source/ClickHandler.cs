using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public event Action OnClicked;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
    }
}
