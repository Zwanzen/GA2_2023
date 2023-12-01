using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer[] destinationLamps;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    private int destination = 0;
    public bool hasDestination = false;

    public void SetDestination(int destination_)
    {
        if (destination != 0)
        {
            destinationLamps[destination - 1].material = offMaterial;
            destination = destination_;
            destinationLamps[destination - 1].material = onMaterial;
            hasDestination = true;
        }
        else
        {
            destination = destination_;
            destinationLamps[destination - 1].material = onMaterial;
            hasDestination = true;
        }
    }
}
