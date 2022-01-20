using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRefreshRate : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Awake()
    {
        OVRPlugin.systemDisplayFrequency = 90.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
