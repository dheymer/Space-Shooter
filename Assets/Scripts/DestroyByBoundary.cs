using UnityEngine;

/**
 * Class that establishes the scene limits and destroys all the elements outside it
 */
public class DestroyByBoundary : MonoBehaviour {
    /**
     * Destroys the objects that exit the scene collider 
     * @param other: The object to be destroyed
     */
    void OnTriggerExit(Collider other){
        Destroy(other.gameObject);
    }
}