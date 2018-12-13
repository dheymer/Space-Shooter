using UnityEngine;

/**
 * Class that establishes the rotation for the asteroids
 */
public class RandomRotator : MonoBehaviour {

    public float tumble;                        // Rotation speed factor
    private Rigidbody rig;                      // Asteroid's Rigidbody

    /**
     * Initialize the component
     */
    void Awake(){
        rig = GetComponent<Rigidbody>();
    }

    /**
     * Use this for initialization
     */
    void Start () {
        // Stablish the rotation direction and velocity for the asteroid...
        /* Vector3 angularVelocity = Random.insideUnitSphere;   new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized; */
        // ...and multiply it by the rotation speed factor
        rig.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
}