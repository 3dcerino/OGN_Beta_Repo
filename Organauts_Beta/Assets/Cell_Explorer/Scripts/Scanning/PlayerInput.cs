using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private ObjectiveUI ui;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Scanner scanner;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private ToggleUI toggleUI;


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
            AdjustInput(currentController == OVRInput.Controller.Hands);
        }

        if (currentController != OVRInput.Controller.Hands) 
        {
            //Right Trigger to move forward, left to move backwards
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                player.RestrictDirection(playerCamera.transform.forward);
            }

            else if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                player.RestrictDirection(-playerCamera.transform.forward);
            }
             
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
            {       
                player.MoveRestricted();
            }



            //Press X to Scan
            /*
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                scanner.SetScanEnabled(true);
            }

            else if (OVRInput.GetUp(OVRInput.Button.Three))
            {
                scanner.StopScan();
            }
            */


        }
    }


    private void AdjustInput(bool isHands)
    {
        ui.AdjustUIToHands(isHands);
        scanner.AdjustTriggerToHands(isHands);
        toggleUI.SetViewAngle(isHands);
        
        //scanner.SetScanEnabled(isHands);

    }
}
