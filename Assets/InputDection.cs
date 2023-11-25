using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDection : MonoBehaviour
{
    public bool CanUseControls { get; set; }

    private void Awake()
    {
        CanUseControls = false;
    }
}
