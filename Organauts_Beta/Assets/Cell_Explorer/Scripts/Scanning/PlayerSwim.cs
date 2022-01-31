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

    public enum HandType
    {
        Left,
        Right
    }

    public void RegisterEnter(float time, TriggerType trigger, HandType hand)
    {
        switch (trigger)
        {
            case TriggerType.Right:

                Debug.Log("Right Enter, phase " + swimPhase);

                if (swimPhase != 1)
                {
                    ResetSwim();
                }

                else
                {
                    rightHandRegistered = true;
                    rightHandTime = time;

                    CheckNextPhase();
                }
                break;


            case TriggerType.Left:

                Debug.Log("Left Enter, phase " + swimPhase);
                if (swimPhase != 1)
                {
                    ResetSwim();
                }

                else
                {
                    leftHandRegistered = true;
                    leftHandTime = time;

                    CheckNextPhase();
                }
                break;


            case TriggerType.Middle:

                Debug.Log("Middle Enter, phase " + swimPhase );

                if (swimPhase != 0)
                {                    
                    ResetSwim();
                }

                else
                {
                    if(hand == HandType.Right)
                    {
                        Debug.Log("Was right hand");
                        rightHandRegistered = true;
                        rightHandTime = time;
                    }

                    else
                    {
                        Debug.Log("Was left hand");
                        leftHandRegistered = true;
                        leftHandTime = time;
                    }

                    if(leftHandRegistered && rightHandRegistered)
                    {
                        NextPhase();
                    }
                }

                break;
        }
    }

    public void RegisterExit(float time, TriggerType trigger)
    {
        Debug.Log("exit, phase " + swimPhase ); 

        if (swimPhase == 2 && trigger != TriggerType.Middle)
        {
            switch (trigger)
            {
                case TriggerType.Right:
                    Debug.Log("Right exit");
                    rightHandTime = time;
                    rightHandRegistered = true;
                    break;


                case TriggerType.Left:
                    Debug.Log("Left Exit");
                    leftHandTime = time;
                    leftHandRegistered = true;
                    break;
            }

            if(rightHandRegistered && leftHandRegistered)
            {
                if(HandsAreSynchronized() && !MovementIsDelayed(time))
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

    private void NextPhase()
    {
        rightHandRegistered = false;
        leftHandRegistered = false;
        lastPhaseTime = Time.time;
        swimPhase++;
    }


    private void CheckNextPhase()
    {
        if (rightHandRegistered && leftHandRegistered)
        {
            if (HandsAreSynchronized())
            {
                NextPhase();
            }

            else
            {
                ResetSwim();
            }
        }
    }

    private bool HandsAreSynchronized()
    {
        return Mathf.Abs(rightHandTime - leftHandTime) <= synchroTolerance;
    }

    private bool MovementIsDelayed(float phaseTime)
    {
        return phaseTime - lastPhaseTime > delayTolerance;
    }
}
