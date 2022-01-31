using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTrigger : MonoBehaviour
{
    [SerializeField] private PlayerSwim.TriggerType triggerType;
    [SerializeField] private PlayerSwim swim;

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;

        if(otherTag.Equals("LeftSwimCube") && (triggerType == PlayerSwim.TriggerType.Left || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterEnter(Time.time, triggerType, PlayerSwim.HandType.Left);
        }

        else if(otherTag.Equals("RightSwimCube") && (triggerType == PlayerSwim.TriggerType.Right || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterEnter(Time.time, triggerType, PlayerSwim.HandType.Right);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string otherTag = other.tag;

        if (otherTag.Equals("LeftSwimCube") && (triggerType == PlayerSwim.TriggerType.Left || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterExit(Time.time, triggerType);
        }

        else if (otherTag.Equals("RightSwimCube") && (triggerType == PlayerSwim.TriggerType.Right || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterExit(Time.time, triggerType);
        }
    }
}
