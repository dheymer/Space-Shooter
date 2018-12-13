using UnityEngine;

/**
 * Class that controls the enemy shoots
 * */
public class WeaponController : MonoBehaviour {

    public GameObject shot;                     // The enemy shot object
    public Transform shotSpawn;                 // The enemy shot transform (position and rotation)
    public float delay;                         // Delay before shooting
    public float fireRate;                      // Firing rate

    private AudioSource audioSource;            // The sound of the shot

    /**
     * Initialization method
     * */
    private void Awake(){
        // Get the audio Source
        audioSource = GetComponent<AudioSource>();
    }

    /**
     * Use this for initialization
     * */
    void Start () {
        InvokeRepeating("Fire", delay, fireRate);
	}
	
    /**
     * Update is called once per frame
     * */
	void Update () {
		
	}
    
    /**
     * Method that fires a shot
     * */
    void Fire() {
        // Instantitates a shot
        Instantiate(shot, shotSpawn.position,shotSpawn.rotation);
        // Play the audio source
        audioSource.Play();
    }
}