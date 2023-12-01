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

    private void Update()
    {
        if(!POWER)
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
