namespace Assets.Source.Scripts.UI.Buttons
{
    public class ReverseButton : StatefulButton
    {
        public void Disable()
        {
            //Button.interactable = false;

            SetState(true);
        }

        public void Enable()
        {
            //Button.interactable = true;

            ResetState();
        }
    }
}
