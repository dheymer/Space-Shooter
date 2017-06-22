using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that establishes the scene limits
 */ 
[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;        //Min and Max values for Player's ship position
}

/**
 * Class that controls the player's ship 
 */
public class PlayerController : MonoBehaviour {

    public float speed;                         //Speed factor to multiply the movement vector
    public float tilt;                         //Rotation factor for the Player's ship when moving in the X axis
    public Boundary boundary;                   //Scene limits
    private Rigidbody rig;                      //Player's ship Rigidbody

	// Use this for initialization
	void Awake () {
        //Obtain the Rigidbody component and use it as a variable
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");     //Horizontal axis movement
        float moveVertical = Input.GetAxis("Vertical");         //Vertical axis movement
        //Create a 3d vector with the movement direction (only X and Z axis, Y axis is not used to move the player's ship)
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        //Assign the velocity to the Player's ship multiplying the direction by the speed factor
        rig.velocity = movement * speed;
        //Update the Player's ship position, always between the scene limits
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax),0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        //Update the player's ship rotation, depending on the speed on the X axis
        rig.rotation = Quaternion.Euler(new Vector3(0f, 0f, rig.velocity.x * -tilt));
	}
}
