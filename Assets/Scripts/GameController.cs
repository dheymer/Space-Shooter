using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Class that controls the main game functions (score, game over status, enemies and asteroids generations, etc)
 */
public class GameController : MonoBehaviour {

    public GameObject[] hazards;                // The enemies and asteroids to be instantiated
    public Vector3 spawnValues;                 // The initial position for hazards
    public int hazardCount;                     // The initial quantity of hazards to form a wave
    public float spawnWait;                     // Time between spawns
    public float startWait;                     // Initial time until the hazards start spawning

    private int score;                          // The score value
    public Text scoreText;                      // The text component that shows the score

    public GameObject restartGameObject;        // Text component to restart after the game is over
    public GameObject gameOverGameObject;       // Text component to indicate that the game is over
    private bool gameOver;                      // Flag that indicates the game is over

	/**
     * Use this for initialization
     */
	void Start () {
        UpdateSpawnValues();
        // Initialize the gameOver and restart flags and texts
        gameOver = false;
        restartGameObject.SetActive(gameOver);
        gameOverGameObject.SetActive(gameOver);
        // Initialize the score value and update the score text
        score = 0;
        UpdateScore();
        // Spawn the asteroids and enemies
        StartCoroutine(SpawnWaves());
	}

    /**
     * Updates the spawn position values on screen for the asteroids
     */
    void UpdateSpawnValues() {
        // Get the screen dimensions
        Vector2 half = Utils.GetHalfWorldDimensions() / 2;
        // Update the vector with the spawn values
        spawnValues = new Vector3(half.x - 0.4f, 0f, half.y + 12f);
    }

    /**
     * Updating the game status
     */
    void Update(){
        // Checking if the 'R' key is pressed after the game is over to restart
        if (gameOver && Input.GetKeyDown(KeyCode.R)) {
            // Restart the game
            Restart();
        }
    }

    /**
     * Restart the game
     */
    public void Restart() {
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**
     * Method (coroutine) used to spawn asteroids and enemies
     */
    IEnumerator SpawnWaves () {
        // Infinite loop
        while (!gameOver){
            // Wait until the start time has passed to start generating hazards
            yield return new WaitForSeconds(startWait);
            // For the quantity of hazards in the wave
            for (int i = 0; i < hazardCount; i++){
                // Randomly pick one kind of hazard 
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                // Establish the position where the hazards are spawned
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0f, spawnValues.z);
                // Instantiate the hazard
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                // Wait until the time between spawn has passed to generate another hazard
                yield return new WaitForSeconds(spawnWait);
            }
        }
	}

    /**
     * Method used to increase the score value
     */
    public void AddScore(int value) {
        score += value;
        UpdateScore();
    }

    /**
     * Method used to update the score
     */
    void UpdateScore(){
        scoreText.text = "Score: " + score;
    }

    /**
     * Method to show the game over message
     */
    public void GameOver() {
        gameOver = true;
        restartGameObject.SetActive(gameOver);
        gameOverGameObject.SetActive(gameOver);
    }
}