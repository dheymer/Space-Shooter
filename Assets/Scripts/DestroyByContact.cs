using UnityEngine;

/**
 * Class that determines when an object enters in other object's space (and destroy both)
 */
public class DestroyByContact : MonoBehaviour {

    public GameObject explosion, playerExplosion;                       // Asteroids and player explosions
    private GameController gameController;                              // Game Controller reference
    public int scoreValue;                                              // The value of the destroyed hazard

    /**
     * Function that initializes the components
     */
    void Start(){
        // Obtain the reference to the Game Controller
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    /**
     * Function that detects the collission
     * @param other: The object that collides
     */
    void OnTriggerEnter(Collider other){
 
        // If the collission is with the boundaries, exit the function
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;

        // Otherwise, check if the Object has an explosion reference and instantiate the explosion...
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        // In case the player collides with the asteroid...
        if (other.CompareTag("Player")){
            // ...Instantiate the player explosion
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            // ...Indicate that the game is over
            gameController.GameOver();
        }
        // ...update the score...
        gameController.AddScore(scoreValue);
        // ...destroy the object that enters in the space of this one...
        Destroy(other.gameObject);
        // ...and destroy this object
        Destroy(gameObject);
    }
}