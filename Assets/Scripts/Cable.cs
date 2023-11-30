using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] private Cable otherEnd;
    public bool IsConnected;
    public bool IsGrabable;

    public bool IsGrabbed = false;
    public WallConnecter connector;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!otherEnd.IsConnected && !IsConnected)
        {
            IsGrabable = true;
        }
        else if(otherEnd.IsConnected)
        {
            IsGrabable = true;
        }
        else
        {
            IsGrabable = false;
        }

        if (rb.isKinematic && !IsGrabbed)
        {
            IsConnected = true;
        }
        else
        {
            IsConnected = false;
        }
    }
}
