using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FluidCapsule : MonoBehaviour
{
    public FluidType fluid = FluidType.Blue;
    [SerializeField]
    private MeshRenderer render;

    public enum FluidType
    {
        Red,
        Green, 
        Blue
    }

    private void Start()
    {
        if(fluid == FluidType.Red)
        {
            render.material.color = Color.red;
        }
        else if(fluid == FluidType.Green)
        {
            render.material.color = Color.green;
        }
        else
        {
            render.material.color = Color.blue;
        }
    }

}
