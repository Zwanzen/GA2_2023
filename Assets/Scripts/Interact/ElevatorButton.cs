using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Elevator Button")]
public class ElevatorButton : Interactable
{

    [SerializeField] 
    private int story;
    private ElevatorManager manager;

    public override void OnActivate()
    {
        manager = GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorManager>();
        manager.SetDestination(story);
    }

}
