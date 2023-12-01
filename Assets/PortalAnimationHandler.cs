using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimationHandler : MonoBehaviour
{

    [SerializeField]
    private float timeDelay = 3f;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject puddle;

    private Animator anim;
    private bool startDelay = false;
    private float t;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ButtonPressed()
    {
        anim.SetTrigger("Start");
    }

    private void StartFireDelay()
    {
        startDelay = true;
    }

    private void Fire()
    {
        laser.SetActive(true);
        puddle.SetActive(true);
    }

    private void Update()
    {
        if(startDelay)
        {
            t += Time.deltaTime; ;

            if(t > timeDelay)
            {
                Fire();
            }
        }
    }

}
