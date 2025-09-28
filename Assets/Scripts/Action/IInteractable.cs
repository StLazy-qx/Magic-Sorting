using System;

public interface IInteractable
{
    event Action Interacted;

    void OnClick();
}
