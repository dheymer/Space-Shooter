using UnityEngine;

/**
 * Class that makes the Background Parallax effect
 **/
public class BGScroller : MonoBehaviour {
    public float scrollSpeed;               // Background moving speed

    private Vector3 startPosition;          // Background initial position
    private float tileSize;                 // Background vertical size

    /**
     * Use this for initialization
     * */
    void Start () {
        // Obtain the background position and size values
        startPosition = transform.position;
        tileSize = transform.localScale.y;
	}
	
    /**
     * Update is called once per frame
     * */
	void Update () {
        // Get the new position of the background
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        // Move the background for the new position
        transform.position = startPosition + Vector3.forward * newPosition;
	}
}
