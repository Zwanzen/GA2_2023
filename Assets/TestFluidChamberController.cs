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
    
    
    private float filledAmount = 0;
    private bool hasCapsule = false;

    private Transform Capsule;
    private FluidCapsule FCapsule;
    private Vector3 startPos;
    private Quaternion startRot;

    private float animDuration = 0.5f;
    float timer = 0;
    bool isReady = false;

    bool canTake;

    private void Start()
    {
        UpdateFillText();
    }

    private void UpdateFillText()
    {
        int roundedFillAmount = (int)Math.Round(filledAmount);
        FilledText.text = String.Format("{0}% ", roundedFillAmount);
    }

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
            timer += Time.deltaTime;
            Capsule.transform.position = Vector3.Lerp(startPos, TargetLocation.position, timer / animDuration);
            Capsule.rotation = Quaternion.Lerp(startRot, TargetLocation.rotation, timer / animDuration);

            if(timer > animDuration && FCapsule.FillAmount > 0)
            {
                var drainAmount = Time.deltaTime * 5;
                if (FCapsule.FillAmount < drainAmount)
                {
                    drainAmount = FCapsule.FillAmount;
                }
                FCapsule.EmptyCapsule(drainAmount);
                filledAmount += drainAmount/5;
                UpdateFillText();
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
