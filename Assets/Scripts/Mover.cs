using UnityEngine;

/**
 * Class that moves the player's shoot
 */
public class Mover : MonoBehaviour {

    private Rigidbody rig;                      // Shoot's Rigidbody 
    public float speed;                         // Shoot's speed factor

    /**
     * Initialize the shoot's structure
     */
    private void Awake(){
        // Obtain the Rigidbody for the shoot
        rig = GetComponent<Rigidbody>();
    }

    /**
     * Use this for initialization
     */
    void Start () {
        // Move the shoot fordward with the specified speed
        rig.velocity = transform.forward * speed;	
	}
}