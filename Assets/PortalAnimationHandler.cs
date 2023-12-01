using MoreMountains.Feedbacks;
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

    [SerializeField]
    private MMF_Player readyFeedback;
    [SerializeField]
    private MMF_Player delayFeedback;
    [SerializeField]
    private MMF_Player fireFeedback;

    bool hasFired = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ButtonPressed()
    {
        anim.SetTrigger("Start");
        readyFeedback.PlayFeedbacks();
    }

    private void StartFireDelay()
    {
        startDelay = true;
        delayFeedback.PlayFeedbacks();
    }

    private void Fire()
    {
        laser.SetActive(true);
        puddle.SetActive(true);
        fireFeedback.PlayFeedbacks();
        readyFeedback.StopFeedbacks();
    }

    private void Update()
    {
        if(startDelay)
        {
            t += Time.deltaTime; ;

            if(t > timeDelay && !hasFired)
            {
                Fire();
                hasFired = true;
            }
        }
    }

}
