using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Portal Button")]
public class FinalButton : Interactable
{
    public override void OnActivate()
    {
        GameObject.FindGameObjectWithTag("MainComputer").GetComponent<PortalHandler>().ActivatePortal();
        
    }
}
