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

    Outline outlineComponent;
    public LineRenderer grabLR;
    public Material LRMaterial;

    [Header("Spring Joint Configurations")]
    [SerializeField]
    private float springForce = 50f;
    [SerializeField]
    private float damper = 1f;
    [SerializeField]
    private float minDist = 0.1f;
    [SerializeField]
    private float maxDist = 1f;
    [SerializeField]
    private float tolarance = 0.02f;


    public Transform lookedAtTransform;

    private void Start()
    {
        outlineComponent = GetComponent<Outline>();
    }

    private void Update()
    {
        HandleOutline();

        HandleGrabInput();
    }

    private void FixedUpdate()
    {
        if (grabbing)
        {
            Holding();
        }
        else
        {
            grabLR.enabled = false;
        }
    }

    private void HandleGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (lookedAtTransform != null)
            {
                Grab(lookedAtTransform);
            }
        }

        if (grabbing && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drop();
        }
    }

    private Transform LookingAtTransform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, GrabLenght, GrabLayer))
        {
            return hit.transform;
        }
        else
        {
            return null;

        }
    }

    private void EnableOutline(Transform transform)
    {
        //If transform dont have an outline component, add it, then fix the settings.
        if(transform.GetComponent<Outline>() == null)
        {
            var newOutline = transform.AddComponent<Outline>();
            newOutline.OutlineColor = outlineComponent.OutlineColor;
            newOutline.OutlineWidth = outlineComponent.OutlineWidth;
            newOutline.OutlineMode = outlineComponent.OutlineMode;
        }

        //Enable the outline, and store the transform to be turned off later.
        transform.GetComponent<Outline>().enabled = true;
        lookedAtTransform = transform;
    }

    private void DisableOutline(Transform transform)
    {
        //Dont need to check if it has outline component, since every transform inputted here has to have had it.
        transform.GetComponent<Outline>().enabled = false;
        lookedAtTransform = null;
    }

    private void HandleOutline()
    {
        //If im currently grabbing an object, i want only the grabbed object to get an outline.
        if(grabbing)
        {
            return;
        }

        //If im looking at a new transform, handle outline.
        if(LookingAtTransform() != null)
        {
            //If there was a previous object, check if the new object is the same.
            if (lookedAtTransform != null)
            {
                //if it's the same, do nothing, if it's a new object, remove the last object's outline, and add outline on the new object.
                if (LookingAtTransform() != lookedAtTransform)
                {
                    DisableOutline(lookedAtTransform);
                    EnableOutline(LookingAtTransform());
                }
            }
            else
            {
                //If no previous object, enable the new objects outline.
                EnableOutline(LookingAtTransform());
            }
        }
        //If im not looking at anything, remove previous outlines if there were any.
        else if(lookedAtTransform != null)
        {
            DisableOutline(lookedAtTransform);
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

        float force = Vector3.Distance(rb.position, HoldPos) * 30f;


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

    private void AddSpringJoint(Transform obj)
    {

    }
}
