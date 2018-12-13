using UnityEngine;

/**
 * Static class for screen redimension
 * */
public static class Utils {
    /**
     * Method that gets the screen dimensions
     **/
    public static Vector2 GetHalfWorldDimensions() {
        float width, height;                    // Screen width and height
        // Obtain the reference of the MainCamera
        Camera cam = Camera.main;
        // Obtain the aspect ratio
        float ratio = cam.pixelWidth / (float)cam.pixelHeight;
        // Obtain the screen height, in units
        height = cam.orthographicSize * 2;
        // Obtain the width, in units, based on the height and the aspect ratio
        width = height * ratio;

        // Return the Vector2 with the screen dimensions
        return new Vector2(width, height) / 2f;
    }
}