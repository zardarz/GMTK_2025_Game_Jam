using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // this will store all of the movements it has to do and do them in order

    private Vector2 movement; // the movement of the ghost

    private float movementSpeed; // the speed of the ghost (should be the same as the players)

    [SerializeField] private Rigidbody2D rb; // the rigidbody of the ghost

    [SerializeField] private Vector2[] movementBuffer; // the movements the ghost has to do

    private int currentMovementIndex; // this will track what index of the movement buffer we are on

    public float timeSinceSpawn; // this stores the time since this ghost spawned

    public void SetMovementSpeed(float speed) { // this sets the movement speed of the ghost
        movementSpeed = speed;
    }

    public void SetMovementBuffer(List<Vector2> movementChanges) { // this will provide the ghost with all of the movements it has to do
        movementBuffer = movementChanges.ToArray();  // turn the movements to an array and set it to the buffer
    }

    void FixedUpdate()
    {
        if(currentMovementIndex >= movementBuffer.Length) { // if the current movement index is more than lenght of the movement buffer
            movement = Vector2.zero; // we set the movement to zero so the ghost can stop moving
        } else { // if not
            movement = movementBuffer[currentMovementIndex]; // set the movement to the current movement buffer thing 
        }

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime); // move the ghost
        currentMovementIndex++; // increment the movement buffer index
    }

    public void ResetGhost() {
        // this will reset the ghost

        timeSinceSpawn = 0f; // reset the timer so the ghost can do all of the movements again
        transform.position = Vector3.zero; // reset the position to the spawn position
        currentMovementIndex = 0; // we reset the current movement index too
    }
}