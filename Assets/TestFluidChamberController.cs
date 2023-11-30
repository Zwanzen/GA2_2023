using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class TestFluidChamberController : MonoBehaviour
{

    public Transform TargetLocation;
    public TextMeshProUGUI FilledText;
    public PhysicsGrabber player;

    public FluidCapsule.FluidType fluidType = FluidCapsule.FluidType.Blue;


    public bool hasCapsule = false;
    public bool IsReady = false;

    private Transform Capsule;
    private FluidCapsule FCapsule;
    private Vector3 startPos;
    private Quaternion startRot;

    private float animDuration = 0.5f;
    float timer = 0;
    bool isReady = false;

    bool canTake;

    private void GrabCapsule(Collider capsule_)
    {
        Capsule = capsule_.transform;
        FCapsule = Capsule.GetComponent<FluidCapsule>();
        startPos = Capsule.position;
        startRot = Capsule.rotation;
        capsule_.GetComponent<Rigidbody>().isKinematic = true;
        hasCapsule = true;

    }

    bool CheckDistTake()
    {
        float dist = Vector3.Distance(player.HoldPos, TargetLocation.position);
        if (dist < 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Process()
    {

        if(player.grabbing)
        {
            //If capsule in machine, and im grabbing it, check dist to se if im dragging it out
            if(hasCapsule)
            {
                if (Capsule == player.lookedAtTransform)
                {
                    canTake = CheckDistTake();
                }
            }
            //If capsule not in machine, im only checking if what im grabbing is close enough
            else
            {
                canTake = CheckDistTake();
            }
        }

        if(hasCapsule && canTake)
        {
            if(timer < 1f)
            {
                timer += Time.deltaTime;
                Capsule.transform.position = Vector3.Lerp(startPos, TargetLocation.position, timer / animDuration);
                Capsule.rotation = Quaternion.Lerp(startRot, TargetLocation.rotation, timer / animDuration);
            }
            else
            {
                if(FCapsule.fluidType == fluidType)
                {
                    isReady = true;
                }
            }

        }
        else
        {
            timer = 0;
            if(Capsule != null)
            {
                Capsule.GetComponent<Rigidbody>().isKinematic = false;
            }
            hasCapsule = false;

        }
    }

    private void Update()
    {
        Process();
    }

    private void OnTriggerStay(Collider other)
    {


        if(other.tag == "Capsule" && !hasCapsule && player.grabbing && canTake)
        {
            if (player.lookedAtTransform == other.transform)
            {
                GrabCapsule(other);
            }
        }
    }
}
