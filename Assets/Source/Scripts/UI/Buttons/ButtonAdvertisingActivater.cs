using YG;

namespace Assets.Source.Scripts.UI.Buttons
{
    public class ButtonAdvertisingActivater : BaseButton
    {
        protected override void OnButtonClick()
        {
            YG2.InterstitialAdvShow();
        }
    }
}