using YG;

namespace Assets.Source.Scripts.UI.Buttons
{
    public class ButtonAdvertisingActivater : BaseMenuButton
    {
        protected override void OnButtonClick()
        {
            YG2.InterstitialAdvShow();
        }
    }
}