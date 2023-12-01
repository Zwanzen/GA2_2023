using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    public TestFluidChamberController fluid1;
    public TestFluidChamberController fluid2;
    public TestFluidChamberController fluid3;
    public PowerController power;
    public DestinationManager destination;

    private bool bool1;
    private bool bool2;
    private bool bool3;
    private bool bool4;
    private bool bool5;

    public bool IsReady;

    public MeshRenderer ReadySignal;
    [SerializeField]
    private Material readyMaterial;

    [SerializeField]
    private MeshRenderer fluidSignal;
    [SerializeField]
    private MeshRenderer powerSignal;
    [SerializeField]
    private MeshRenderer destinationSignal;

    [SerializeField]
    private Animator ButtonAnim;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckIfReady();
    }

    private void CheckIfReady()
    {
        bool1 = fluid1.IsReady;
        bool2 = fluid2.IsReady;
        bool3 = fluid3.IsReady;
        bool4 = power.HasPower;
        bool5 = destination.hasDestination;

        if(bool1 && bool2 && bool3 && bool4 && bool5 && !IsReady)
        {
            IsReady = true;
            ReadySignal.material = readyMaterial;
        }

        if(bool1 && bool2 && bool3)
        {
            fluidSignal.material = readyMaterial;  
        }

        if (bool4)
        {
            powerSignal.material = readyMaterial;
        }

        if (bool5)
        {
            destinationSignal.material = readyMaterial;
        }
    }

    public void ActivatePortal()
    {
        ButtonAnim.SetTrigger("Pressed");

        if(IsReady)
        {
            //Start portal sequence
        }
    }
}
