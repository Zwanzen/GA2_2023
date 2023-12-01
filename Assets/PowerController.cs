using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    public bool HasPower;
    public bool POWER;

    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private MeshRenderer light;

    [SerializeField]
    private WallConnecter[] wallConnecters;
    [SerializeField]
    MMF_Player feedback;

    private void Update()
    {
        if(!POWER && !HasPower)
        {
            bool power = true;
            foreach (WallConnecter con in wallConnecters)
            {
                if (!con.IsPowered)
                {
                    power = false;
                }
            }

            if (power)
            {
                feedback.PlayFeedbacks();
                HasPower = true;
            }
        }
        else
        {

            HasPower = true;
        }

        if(HasPower)
        {
            light.material = onMaterial;
        }
        else
        {
            light.material = offMaterial;
        }

    }

}
