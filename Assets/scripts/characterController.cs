using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour {
    public float speed;
    public Joystick joystick;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movementVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        
        // move only on non-zero vector
        if(movementVector != Vector3.zero) {
            GetComponent<Animator>().SetBool("isMoving", true);
            transform.rotation = Quaternion.LookRotation(movementVector);
            transform.Translate(movementVector * speed * Time.deltaTime, Space.World);
        }
        else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
}
