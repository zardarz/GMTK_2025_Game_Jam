using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // this script will let the player control the player

    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb; // the rigidbody of the player

    [Header("Settings")]
    [SerializeField] private float movementSpeed; // the movement speed of the player

    private Vector2 movement;

    void Update()
    {
        movement = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // get which way the player wants to move
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime); // move the player
    }
}