using UnityEngine;

public class InterpolatedFollowTransform : MonoBehaviour
{
    enum InterpolateMode { Position, Rotation, Both }

    public Transform Target;

    [SerializeField]
    InterpolateMode interpolateMode;

    [SerializeField]
    float positionSpeed = 8;

    [SerializeField]
    float rotationSpeed = 8;

    private void Update()
    {
        if (interpolateMode == InterpolateMode.Position || interpolateMode == InterpolateMode.Both)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, positionSpeed * Time.deltaTime);
        }

        if (interpolateMode == InterpolateMode.Rotation || interpolateMode == InterpolateMode.Both)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}