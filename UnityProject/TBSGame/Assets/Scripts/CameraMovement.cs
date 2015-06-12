using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float cameraMoveThreshold;
    public float cameraMovementSpeed;
    float screenWidth;

	// Use this for initialization
	void Start () {
        screenWidth = Screen.width;
	}

	// Update is called once per frame
	void Update () {
        float mousePosition = Input.mousePosition.x;
        if ((cameraMoveThreshold > mousePosition) && (gameObject.transform.position.x > -8))
        {
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x -= cameraMovementSpeed;
            gameObject.transform.position = newPosition;
        }
        else
        if ((screenWidth - cameraMoveThreshold < mousePosition) && (gameObject.transform.position.x < 8))
        {
            
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x += cameraMovementSpeed;
            gameObject.transform.position = newPosition;
        }
	}
}
