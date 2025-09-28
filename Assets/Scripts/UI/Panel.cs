using UnityEngine;

public class Panel : MonoBehaviour
{
    //private const float StopTime = 0;
    //private const float ContinueTime = 1;

    public void Close()
    {
        gameObject.SetActive(false);

        //Time.timeScale = ContinueTime;
    }

    public void Open()
    {
        gameObject.SetActive(true);

        //Time.timeScale = StopTime;
    }
}
