using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float speed;
    public Joystick joystick;
    public float playerMaxLightIntensity;
    public float lightDecayRate;
	
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
        lightDecay();    }

    private void OnCollisionEnter(Collision collision) {
        //reset lightIntensity
        if (collision.gameObject.CompareTag("lightorb")) {
            playerMaxLightIntensity = 3;
            Destroy(collision.gameObject);
        }
    }

    private void lightDecay() {
        //only detuct till 0
        if(playerMaxLightIntensity >= 0) {
            playerMaxLightIntensity -= lightDecayRate * Time.deltaTime;
        }
    }

}
