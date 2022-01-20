using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifySwimLeft : MonoBehaviour
{
    public bool swimEnableLeft = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MoveCube_L")
        {
            swimEnableLeft = true;
            //Debug.Log("LEFT SWIM");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MoveCube_L")
        {
            swimEnableLeft = false;
        }
    }
}
