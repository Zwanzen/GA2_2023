using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PhysicsGrabber : MonoBehaviour
{

    public LayerMask GrabLayer;
    public float GrabLenght = 1f;
    public Vector3 HoldPos;

    float rotationSpeed = 0.5f;
    public bool grabbing = false;
    Rigidbody rb;

    public Transform lookingAtTransform;
    Outline outlineComponent;
    public LineRenderer grabLR;
    public Material LRMaterial;

    private void Start()
    {
        outlineComponent = GetComponent<Outline>();
    }

    private void Update()
    {
        RaycastHit hit1;
        if (Physics.Raycast(transform.position, transform.forward, out hit1, GrabLenght, GrabLayer) && !grabbing)
        {
            if(lookingAtTransform != null && lookingAtTransform != hit1.transform)
            {
                lookingAtTransform.GetComponent<Outline>().enabled = false;
                lookingAtTransform = null;
            }
            lookingAtTransform = hit1.transform;
            if(lookingAtTransform.GetComponent<Outline>() != null )
            {
                lookingAtTransform.GetComponent<Outline>().enabled = true;

            }
            else
            {
                lookingAtTransform.AddComponent<Outline>();
                var to = lookingAtTransform.GetComponent<Outline>();
                to.OutlineColor = outlineComponent.OutlineColor;
                to.OutlineWidth = outlineComponent.OutlineWidth;
                to.OutlineMode = outlineComponent.OutlineMode;
            }
        }
        else if(!grabbing)
        {
            if(lookingAtTransform != null)
            {
                lookingAtTransform.GetComponent<Outline>().enabled = false;
                lookingAtTransform = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(lookingAtTransform != null)
            {
                Grab(lookingAtTransform);
            }
        }

        if(grabbing && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drop();
        }

        if(grabbing)
        {
            Holding();
        }
        else
        {
            grabLR.enabled = false;
        }
    }

    private void Grab(Transform obj)
    {
        grabbing = true;
        rb = obj.GetComponent<Rigidbody>();
        rb.drag = 5;
        rb.angularDrag = 1f;
    }

    void Holding()
    {
        HoldPos = transform.position + transform.forward * GrabLenght;
        var moveDir = HoldPos - rb.transform.position;

        var force = Vector3.Distance(rb.position, HoldPos) * 10;

        rb.AddForce(moveDir.normalized * force);
        var toRotation = Quaternion.FromToRotation(rb.rotation.eulerAngles, Vector3.zero);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, toRotation, rotationSpeed * Time.deltaTime));

        grabLR.enabled = true;
        grabLR.SetPosition(0,HoldPos);
        grabLR.SetPosition(1, rb.position);

        float dist = Vector3.Distance(HoldPos, rb.position);
        float t = Mathf.Clamp01(dist / GrabLenght); // Ensure t is between 0 and 1
        Color color = Color.Lerp(Color.green, Color.red, t);
        LRMaterial.color = color;

        float w = Mathf.Lerp(0.05f, 0.005f, t);
        grabLR.startWidth = w;

        if (dist > GrabLenght)
        {
            Drop();
        }
    }

    public void Drop()
    {
        rb.drag = 0f;
        rb.angularDrag = 0.1f;
        grabbing = false;

    }
}
