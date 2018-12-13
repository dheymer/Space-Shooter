using UnityEngine;

/**
 * Class that determines the duration of the explosion prefab objects
 */
public class DestroyByTime : MonoBehaviour {

    public float lifeTime;              // Duration time for the explosion effects

	/**
     * Use this for initialization
     */
	void Start () {
        // Destroy the explosion object after the duration expires
        Destroy(gameObject, lifeTime);
	}
}