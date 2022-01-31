using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehaviours : MonoBehaviour
{
    public Transform head;
    public Transform rightHand;
    public Transform leftHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            marker.transform.localScale = Vector3.one * 0.1f;
            marker.transform.SetParent(head);
            marker.transform.position = rightHand.transform.position;
            Debug.Log("Right Hand: " + marker.transform.localPosition);
        }

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.localScale = Vector3.one * 0.1f;
            marker.transform.SetParent(head);
            marker.transform.position = leftHand.transform.position;
            Debug.Log("Left Hand: " + marker.transform.localPosition);
        }

    }
}
