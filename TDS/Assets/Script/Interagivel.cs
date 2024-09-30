using UnityEngine;

public interface IInteractable
{
    string GetInteractionText(bool isOpen);
    void Interact();
}
