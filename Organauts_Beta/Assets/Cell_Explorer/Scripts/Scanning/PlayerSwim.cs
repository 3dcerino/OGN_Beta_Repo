using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwim : MonoBehaviour
{
    [SerializeField] private float synchroTolerance = 0.4f;
    [SerializeField] private float delayTolerance = 0.7f;
    [SerializeField] private float swimForce = 150f;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform playerCam;

    private byte swimPhase = 0;
    float rightHandTime = 0;
    float leftHandTime = 0;
    float lastPhaseTime = 0;
    bool rightHandRegistered = false;
    bool leftHandRegistered = false;

    public enum TriggerType
    {
        Middle,
        Left,
        Right
    }


    public void RegisterEnter(float time, TriggerType trigger)
    {

    }

    public void RegisterExit(float time, TriggerType trigger)
    {
        if(swimPhase == 2 && trigger != TriggerType.Middle)
        {
            switch (trigger)
            {
                case TriggerType.Right:
                    rightHandTime = time;
                    rightHandRegistered = true;
                    break;

                case TriggerType.Left:
                    leftHandTime = time;
                    leftHandRegistered = true;
                    break;
            }

            if(rightHandRegistered && leftHandRegistered)
            {
                if(HandsAreSynchronized() && MovementIsntDelayed(time))
                {
                    SwimSuccesful();                   
                }

                else
                {
                    ResetSwim();
                }
            }

        }
    }


    private void SwimSuccesful()
    {
        player.Impulse(playerCam.forward, swimForce);
        ResetSwim();
    }

    private void ResetSwim()
    {
        rightHandRegistered = false;
        leftHandRegistered = false;
        swimPhase = 0;
    }

    private bool HandsAreSynchronized()
    {
        return Mathf.Abs(rightHandTime - leftHandTime) <= synchroTolerance;
    }

    private bool MovementIsntDelayed(float phaseTime)
    {
        return phaseTime - lastPhaseTime <= delayTolerance;
    }
}
