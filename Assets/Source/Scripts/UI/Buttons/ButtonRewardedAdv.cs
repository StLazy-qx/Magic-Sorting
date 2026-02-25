namespace Assets.Source.Scripts.UI.Buttons
{
    public class ButtonRewardedAdv : StatefulButton
    {
        private void Start()
        {
            Disable();
        }

        public void Disable()
        {
            Button.gameObject.SetActive(false);
        }

        public void Enable()
        {
            Button.gameObject.SetActive(true);
        }
    }
}
