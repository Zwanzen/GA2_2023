using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

public class WallConnecter : MonoBehaviour
{
    [Header("Cable")]
    public Transform Cable;
    private Rigidbody rb;
    [SerializeField]private Transform cableOffset;
    public bool HasCable;
    private ConnectionColor.CableColor wallConnectorColor;
    public bool IsPowered = false;

    private float t;
    private float cableSpeed = 5f;
    private Vector3 oldPos;
    private quaternion startRot;

    private void Start()
    {
        if (Cable !=  null)
        {
            ApplyCable(Cable);
        }

        wallConnectorColor = GetComponent<ConnectionColor>().ConnectionColor_;
    }

    private void Update()
    {
        if (HasCable)
        {
            if (oldPos == Vector3.zero)
            {
                oldPos = rb.transform.position;
                startRot = rb.transform.rotation;
            }

            t += Time.deltaTime * cableSpeed;
            rb.transform.position = Vector3.Lerp(oldPos, cableOffset.position, t);
            rb.transform.rotation = Quaternion.Lerp(startRot, cableOffset.rotation, t);
        }
        else
        {
            t = 0f;
            oldPos = Vector3.zero;
            startRot = Quaternion.identity;
        }
    }

    public void ApplyCable(Transform Cable_)
    {
        Cable = Cable_;
        rb = Cable.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        HasCable = true;
        rb.GetComponent<Cable>().connector = this;

        if(Cable.GetComponent<ConnectionColor>().ConnectionColor_ == wallConnectorColor)
        {
            IsPowered = true;
        }
    }

    public void RemoveCable()
    {
        rb.GetComponent<Cable>().connector = null;
        rb.isKinematic = false;
        HasCable = false;
        Cable = null;
        rb = null;
        IsPowered = false;
    }
}
