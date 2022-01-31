using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce = 1.5f;
    [SerializeField] private Rigidbody player;

    private Vector3 restrictedDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveRestricted()
    {
        player.AddRelativeForce(restrictedDirection);
    }

    public void RestrictDirection(Vector3 direction)
    {
        restrictedDirection = direction.normalized;
    }

    public void Move(Vector3 direction)
    {
        player.AddRelativeForce(direction.normalized);
    }

}
