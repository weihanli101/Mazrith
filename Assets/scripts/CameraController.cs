using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject playerGameObj;

    private Vector3 cameraOffest;


	// Use this for initialization
	void Start () {
        cameraOffest = transform.position - playerGameObj.transform.position;	
	}

    //LateUpdate called after update (so the player moves then the camera follows)
    private void LateUpdate() {
        transform.position = playerGameObj.transform.position + cameraOffest;
    }
}
