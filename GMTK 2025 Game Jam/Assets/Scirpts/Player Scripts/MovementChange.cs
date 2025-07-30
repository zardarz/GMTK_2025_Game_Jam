using UnityEngine;

[System.Serializable]
public class MovementChange
{
    // this class will store when the movement changed and what it changed to

    [SerializeField] private Vector2 newMovement; // the vector it changed to

    [SerializeField] private float timeOfChange; // what time the movement changed

    public MovementChange(Vector2 newMovement, float timeOfChange) { // contructor to set the new movement and the time it happened
        this.newMovement = newMovement;
        this.timeOfChange = timeOfChange;
    }

    public Vector2 GetNewMovement() {
        // returns the new movement
        return newMovement;
    }

    public float GetTimeOfChange() {
        // returns when the change will happen
        return timeOfChange;
    }
}