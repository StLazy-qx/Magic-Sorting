using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHideClicker : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
