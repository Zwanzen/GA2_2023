using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Destination Button")]
public class DestinationButton : Interactable
{
    [SerializeField]
    private int destination;
    private DestinationManager manager;
    public override void OnActivate()
    {
        manager = GameObject.FindGameObjectWithTag("MainComputer").GetComponent<DestinationManager>();
        manager.SetDestination(destination);
    }
}
