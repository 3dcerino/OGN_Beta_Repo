using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private ObjectiveUI ui;

    private OVRInput.Controller currentController = OVRInput.Controller.None;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetActiveController() != currentController)
        {
            currentController = OVRInput.GetActiveController();
            ui.AdjustUIToHands(currentController == OVRInput.Controller.Hands);
        }    
    }
}
