using MoreMountains.Feedbacks;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public Interactable interact;
    [SerializeField]
    MMF_Player soundFeedback;


    public void Interact()
    {
        interact.OnActivate();
        soundFeedback.PlayFeedbacks();
    }
}
