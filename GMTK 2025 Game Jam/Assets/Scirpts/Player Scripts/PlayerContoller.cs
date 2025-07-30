using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // this script will let the player control the player

    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb; // the rigidbody of the player

    [SerializeField] private GameObject ghost; // the ghost the player can spawn

    [Header("Settings")]
    [SerializeField] private float movementSpeed; // the movement speed of the player

    private Vector2 movement; // this stores the movement vector of the player;

    private Vector2 previousMovement; // this store the movement vector of the last frame

    [SerializeField] private List<MovementChange> movementBuffer; // this will store all of the movements the player made

    private float timeSinceLastSpawn; // this is the time since the last ghost was spawned

    private List<GhostController> ghosts = new List<GhostController>(); // all of the ghost that are currently active

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime; // update the time since last ghost spawn

        movement = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // get which way the player wants to move

        TrackMovements(); // track the movements of the player

        if(Input.GetKeyDown(KeyCode.LeftShift)) { // if the player presses left shift
            MakeGhost(); // we make a ghost
        }
    }

    private void TrackMovements() {
        if(movement != previousMovement) { // if the last frames movement is different than this frame
            MovementChange newMovementChange = new MovementChange(movement, timeSinceLastSpawn); // make a new movement change that has the current movement vector and the time since last ghost spawn

            movementBuffer.Add(newMovementChange); // add it to the buffer
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime); // move the player
        previousMovement = movement; // the last movement was this movement
    }

    private void MakeGhost() {
        transform.position = Vector3.zero; // reset the players position
        ResetAllGhost(); // reset all of the ghosts

        GameObject instantiatedGhost = Instantiate(ghost); // make a new ghost

        GhostController ghostController = instantiatedGhost.GetComponent<GhostController>(); // get the ghost controller
        ghosts.Add(ghostController); // add the ghost to the ghosts

        ghostController.SetMovementSpeed(movementSpeed); // set the ghosts movement speed to the players 

        ghostController.SetMovementBuffer(movementBuffer); // set the movement buffer to the current players movement buffer 

        movementBuffer.Clear(); // clear the movement buffer 
        timeSinceLastSpawn = 0f; // reset the timer 
    }

    private void ResetAllGhost() {
        // this will reset every ghost that we have

        for(int i = 0; i < ghosts.Count; i++) { // go for each ghost
            GhostController ghostController = ghosts[i]; // get the ghost controller

            ghostController.ResetGhost(); //  reset the ghost
        }
    }
}