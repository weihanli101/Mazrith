using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float speed;
    public Joystick joystick;
    public float playerMaxLightIntensity;
    public float playerMinLightIntenstiy;
    public float lightDecayRate;
    public int health;
    public float AttackRadius;
    public float AttackPower;
    public float AttackDuration;

    //Audio
    public AudioClip hitAudioClip;
    public AudioClip orbAudioClip;
    private AudioSource audioSource;

    //Particles
    public GameObject bloodParticles;
    public GameObject explosionParticles;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        health = 100;
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
        lightDecay();    }

    private void OnCollisionEnter(Collision collision) {
        //reset lightIntensity
        if (collision.gameObject.CompareTag("lightorb")) {
            explosionParticles.GetComponent<ParticleSystem>().Play();
            playerMaxLightIntensity = 50;
            audioSource.PlayOneShot(orbAudioClip);
            Destroy(collision.gameObject);
            explode();
        }
        //hit enemy: lose health + play hit audio + play blood particles
        if(collision.gameObject.CompareTag("enemy")) {
            bloodParticles.GetComponent<ParticleSystem>().Play();
            audioSource.PlayOneShot(hitAudioClip);
            if(health > 0) {
                health -= 10;
            }
        }
    }

    private void lightDecay() {
        //only detuct till 0
        if(playerMaxLightIntensity >= playerMinLightIntenstiy) {
            playerMaxLightIntensity -= lightDecayRate * Time.deltaTime;
        }
    }

    //adds explosive force to nearby enemies
    private void explode() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, AttackRadius);
        foreach(Collider hit in colliders) {
            Rigidbody enemyRb = hit.GetComponent<Rigidbody>();

            if (enemyRb != null && enemyRb.CompareTag("enemy"))  {
                enemyRb.AddExplosionForce(AttackPower, explosionPos, AttackRadius, AttackDuration);
            }
        }
    }

}
