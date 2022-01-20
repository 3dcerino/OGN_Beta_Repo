using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifySwimRight : MonoBehaviour
{
    public bool swimEnableRight = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MoveCube_R")
        {
            swimEnableRight = true;
            //Debug.Log("RIGHT SWIM");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MoveCube_R")
        {
            swimEnableRight = false;
        }
    }
}
