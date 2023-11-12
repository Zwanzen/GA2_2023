using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FluidCapsule : MonoBehaviour
{

    public float FillAmount = 100f;
    public TextMeshProUGUI FillText;
    public FluidType fluidType = FluidType.Blue;

    public enum FluidType
    {
        Red,
        Green, 
        Blue
    }

    private void UpdateFillText()
    {
        int roundedFillAmount = (int)Math.Round(FillAmount);
        FillText.text = String.Format("{0}% ", roundedFillAmount);
    }

    public void EmptyCapsule(float drainAmount)
    {
        FillAmount -= drainAmount;
        UpdateFillText();
    }

}
