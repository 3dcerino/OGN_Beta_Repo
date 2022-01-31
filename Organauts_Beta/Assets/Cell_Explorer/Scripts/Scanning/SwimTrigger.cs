using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTrigger : MonoBehaviour
{
    [SerializeField] private PlayerSwim.TriggerType triggerType;
    [SerializeField] private PlayerSwim swim;

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if(tag.Equals("LeftScanCube") && (triggerType == PlayerSwim.TriggerType.Left || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterEnter(Time.time, triggerType);
        }

        else if(tag.Equals("RightScanCube") && (triggerType == PlayerSwim.TriggerType.Right || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterEnter(Time.time, triggerType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tag.Equals("LeftScanCube") && (triggerType == PlayerSwim.TriggerType.Left || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterExit(Time.time, triggerType);
        }

        else if (tag.Equals("RightScanCube") && (triggerType == PlayerSwim.TriggerType.Right || triggerType == PlayerSwim.TriggerType.Middle))
        {
            swim.RegisterExit(Time.time, triggerType);
        }
    }
}
