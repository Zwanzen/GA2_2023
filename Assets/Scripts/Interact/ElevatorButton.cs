using UnityEngine;

[CreateAssetMenu(menuName = "Interactable")]
public class ElevatorButton : Interactable
{

    [SerializeField] private int story;

    public override void OnActivate()
    {
        GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorManager>().SetDestination(story);
    }

}
