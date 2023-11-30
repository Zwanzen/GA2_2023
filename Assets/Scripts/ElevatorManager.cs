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
            }
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate the direction towards the target position
        Vector3 direction = destination - elevator.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the transform towards the target position at the specified speed
        elevator.Translate(direction * elevatorSpeed * Time.deltaTime);
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
        previousDestination = transform.position;
        destination = transform.position;
        anim = GetComponent<Animator>();
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
