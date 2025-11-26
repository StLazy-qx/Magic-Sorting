using System;

namespace Assets.Source.Scripts.ActionHandlers
{
    public interface IInteractable
    {
        event Action Interacted;

        void OnClick();
    }
}