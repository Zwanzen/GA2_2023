using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [Header("Elevator Variables")]
    [SerializeField]
    private Transform elevator;
    [SerializeField]
    private float elevatorSpeed = 1f;
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private int story;
    [SerializeField]
    private Transform[] elevatorDestinations;
    [SerializeField]
    private Animator[] destinationDoors;

    private Vector3 previousDestination;
    private Vector3 destination;
    private float t;
    private Animator anim;

    private float elevatorTimer = 0f;

    public void SetDestination(int story_)
    {
        if(!isMoving && story != story_)
        {
            destinationDoors[story - 1].SetBool("Open", false);
            story = story_;
            destination = elevatorDestinations[story - 1].position;
            previousDestination = elevator.position;
            t = 0f;
            canMove = false;
        }
    }

    private void MoveElivator()
    {
        if (!canMove)
        {
            t += Time.deltaTime * 1f;
            anim.SetBool("Open", false);
            if (t > 1f)
            {
                canMove = true;
                t = 0f;
            }
        }
        else
        {
            var dist = Vector3.Distance(elevator.position, destination);
            if(dist > 0.01f)
            {
                MoveTowardsTarget();
            }
            else
            {
                anim.SetBool("Open", true);
                destinationDoors[story - 1].SetBool("Open", true);
                elevatorTimer = 0f;
            }
        }
    }

    void MoveTowardsTarget()
    {
        elevatorTimer += Time.deltaTime * elevatorSpeed;

        // Move the transform towards the target position at the specified speed
        elevator.position = Vector3.MoveTowards(previousDestination, destination, elevatorTimer);
    }

    public void ReadyToMove()
    {
        canMove = true;
    }

    private void Update()
    {
        MoveElivator();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        previousDestination = transform.position;
        destination = elevatorDestinations[story - 1].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        }
        other.transform.SetParent(transform);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
        }
        other.transform.SetParent(null);

    }
}
