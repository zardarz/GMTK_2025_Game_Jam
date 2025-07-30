using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // this will store all of the movements it has to do and do them in order

    private Vector2 movement; // the movement of the ghost

    private float movementSpeed; // the speed of the ghost (should be the same as the players)

    [SerializeField] private Rigidbody2D rb; // the rigidbody of the ghost

    [SerializeField] private MovementChange[] movementBuffer; // the movements the ghost has to do

    private int currentMovementIndex; // this will track what index of the movement buffer we are on

    private bool hasMovementBuffer = false; // this indicated wheather this ghost has the movement buffer or not

    public float timeSinceSpawn; // this stores the time since this ghost spawned

    public void SetMovementSpeed(float speed) { // this sets the movement speed of the ghost
        movementSpeed = speed;
    }

    public void SetMovementBuffer(List<MovementChange> movementChanges) { // this will provide the ghost with all of the movements it has to do
        movementBuffer = movementChanges.ToArray();  // turn the movements to an array and set it to the buffer
        hasMovementBuffer = true; // we now have the movement buffer
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime); // move the ghost
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime; // update the timer

        if(currentMovementIndex >= movementBuffer.Length || hasMovementBuffer == false) { // if we are at the last movement change or we dont have the movement buffer
            movement = Vector2.zero; // we stop the ghost from moving
            return; // make the ghost do nothing else
        }

        MovementChange nextMovementChange = movementBuffer[currentMovementIndex + 1]; // get the next movement change

        if(timeSinceSpawn >= nextMovementChange.GetTimeOfChange()) { // if the current time is greater than or equal to the next movement changes time
            currentMovementIndex += 1; // the current movement change is the next movement change
        }
        
        movement = movementBuffer[currentMovementIndex].GetNewMovement(); // set the movement vector to the current movement change
    }

    public void ResetGhost() {
        // this will reset the ghost

        timeSinceSpawn = 0f; // reset the timer so the ghost can do all of the movements again
        transform.position = Vector3.zero; // reset the position to the spawn position
        currentMovementIndex = 0; // we reset the current movement index too
    }
}