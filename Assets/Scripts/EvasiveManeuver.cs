using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;                   // Time range before starting the evasive maneuvers
    public Vector2 maneuverTime;                // Time range before stopping the evasive maneuvers
    public Vector2 maneuverWait;                // Time range before starting the next evasive maneuvers
    public float smoothing;                     // The acceleration factor
    public float dodge;                         // The max movement speed to make the evasive maneuvers
    public float tilt;                          // Rotation factor for the Player's ship when moving in the X axis
    public Boundary boundary;                   // Scene limits

    private float targetManeuver;               // The sideways movement speed
    private Rigidbody rb;                       // The ship object's RigidBody

    /**
     * Awaking method
     * */
    void Awake(){
        // Obtain the reference to the ship's RigidBody
        rb = GetComponent<Rigidbody>();

    }

    /**
     * Method to Update boundaries
     */
    void UpdateBoundary(){
        // Obtain the screen size and divide it by 2
        Vector2 half = Utils.GetHalfWorldDimensions();
        // Set the width dimensions
        boundary.xMin = -half.x + 0.4f;
        boundary.xMax = half.x - 0.4f;
        // Set the height dimensions
        boundary.zMin = -half.y + 7f;
        boundary.zMax = half.y - 1f;
    }

    /**
     * Use this for initialization
     * */
    void Start () {
        //Update the scene limits
        UpdateBoundary();
        // Invokes the coroutine
        StartCoroutine(Evade());
	}

    /**
     * Coroutine that makes the enemy ships do evasive maneuvers
     * */
    IEnumerator Evade(){
        // Start the waiting before the movement
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        // Infinite loop to make the evasive maneuvers
        while (true){
            // Establish a random dodge speed and direction depending on the ship's position
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            // Start the waiting to stop the movement
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            // Stop the evasive maneuver
            targetManeuver = 0;
            // Start the waiting for the next movement
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	/**
     * Update is called once per frame
     * */
	void FixedUpdate () {
        // Set the acceleration of the movement towards the movement speed
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        // Assign the movement velocity to the ship
        rb.velocity = new Vector3(newManeuver, 0.0f, rb.velocity.z);
        // Update the ship's position, always between the scene limits
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), rb.position.y, rb.position.z);
        // Update the ship's rotation, depending on the speed on the X axis
        rb.rotation = Quaternion.Euler(new Vector3(0f, 0f, rb.velocity.x * -tilt));
    }
}
