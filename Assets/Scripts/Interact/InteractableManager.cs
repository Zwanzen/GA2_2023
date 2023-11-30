using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public Interactable interact;

    public void Interact()
    {
        interact.OnActivate();
    }
}
