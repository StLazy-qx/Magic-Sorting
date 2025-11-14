using System;

namespace ActionHandler
{
    public interface IInteractable
    {
        event Action Interacted;

        void OnClick();
    }
}