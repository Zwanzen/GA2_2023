using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

public class WallConnecter : MonoBehaviour
{
    [Header("Cable")]
    public Transform Cable;
    [SerializeField]private Transform cableOffset;
    public bool HasCable;

    private float t;
    private float cableSpeed = 5f;
    private Vector3 oldPos;
    private quaternion startRot;

    private void Update()
    {
        if (HasCable)
        {
            var rb = Cable.GetComponent<Rigidbody>();

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
        HasCable = true;
        Cable = Cable_;
        KinemaCable(true);
        
    }

    public void RemoveCable()
    {
        KinemaCable(false);
        HasCable = false;
        Cable = null;
    }

    void KinemaCable(bool b)
    {
        var rb = Cable.GetComponent<Rigidbody>();
        rb.isKinematic = b;
    }
}
