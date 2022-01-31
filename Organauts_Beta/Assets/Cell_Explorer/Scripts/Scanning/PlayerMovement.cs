using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce = 90f;
    [SerializeField] private Rigidbody player;

    private Vector3 restrictedDirection;

    
    public void RestrictDirection(Vector3 direction)
    {
        restrictedDirection = direction.normalized;
    }

    public void MoveRestricted()
    {
        player.AddRelativeForce(restrictedDirection * movementForce * Time.deltaTime);
    }


    public void Move(Vector3 direction)
    {
        player.AddRelativeForce(direction.normalized * movementForce * Time.deltaTime);
    }

    public void Impulse(Vector3 direction, float movForce)
    {
        player.AddRelativeForce(direction.normalized * movForce, ForceMode.Impulse);
    }

}
