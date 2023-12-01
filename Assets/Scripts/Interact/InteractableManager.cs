using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public Interactable interact;

    public void Interact()
    {
        interact.OnActivate();
    }
}
