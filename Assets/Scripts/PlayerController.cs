using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/**
 * Class that establishes the scene limits
 */
[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;        // Min and Max values for Player's ship position
}

/**
 * Class that controls the player's ship 
 */
public class PlayerController : MonoBehaviour {

    [Header("Movement")]
    public float speed;                         // Speed factor to multiply the movement vector
    public float tilt;                          // Rotation factor for the Player's ship when moving in the X axis
    public Boundary boundary;                   // Scene limits
    private Rigidbody rig;                      // Player's ship Rigidbody

    [Header("Shooting")]
    public GameObject shot;                     // Player's shoot
    public Transform shotSpawn;                 // Player's shoot generation
    public float fireRate;                      // Player's fire rate
    private float nextFire;                     // Time between shoots

	/**
     * Use this for initialization
     */
	void Awake () {
        // Obtain the Rigidbody component and use it as a variable
        rig = GetComponent<Rigidbody>();
	}

    /**
     * Creator Method
     */
    void Start(){
        // Update the screen limits
        UpdateBoundary();
    }

    /**
     * Method to Update boundaries
     */
    void UpdateBoundary() {
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
     * Update is called once per frame (Actions)
     */
    private void Update(){
        // If the player press the fire key and the time for the next shoot has passed
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time >= nextFire) {
            // Updates the time for the next shoot
            nextFire = Time.time + fireRate;
            // Generates a shoot
            Instantiate(shot, shotSpawn.position, Quaternion.identity, shotSpawn);
        }
    }

    /**
     * Update is called once per frame (Physics)
     */
    void FixedUpdate () {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");     // Horizontal axis movement
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");         // Vertical axis movement
        // Create a 3d vector with the movement direction (only X and Z axis, Y axis is not used to move the player's ship)
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        // Assign the velocity to the Player's ship multiplying the direction by the speed factor
        rig.velocity = movement * speed;
        // Update the Player's ship position, always between the scene limits
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax),0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        // Update the player's ship rotation, depending on the speed on the X axis
        rig.rotation = Quaternion.Euler(new Vector3(0f, 0f, rig.velocity.x * -tilt));
	}
}