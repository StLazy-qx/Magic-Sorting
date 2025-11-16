using YG;

public class ButtonAdvertisingActivater : BaseMenuButton
{
    protected override void OnButtonClick()
    {
        YG2.InterstitialAdvShow();
    }
}
