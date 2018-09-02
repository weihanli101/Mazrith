using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int moveSpeed;
    public float rotationDamping;
    [Range(0, 100)]
    public float pausePercentage;
    public float alertRadius;
    public float alertedMovementSpeed;
    public GameObject player = null;

    private float timeToChange;
    private float desiredRotation;
    private float randomSeed;
    private bool isAlerted;
    private bool isMoving;

    void Start() {
        ChangeDirection(false);
        randomSeed = Random.Range(0, 100);
        if(player == null) {
            player = GameObject.Find("player");
        }
    }

    void Update() {
        //alerted AI moves towards player
        if(Vector3.Distance(this.transform.position, player.GetComponent<Transform>().position) <= alertRadius) {
            transform.LookAt(player.GetComponent<Transform>());
            transform.position += transform.forward * alertedMovementSpeed * Time.deltaTime;
            isAlerted = true;
            isMoving = true;
        }
        else {
            isAlerted = false;
        }
        // roaming mode when AI is not alerted
        if(!isAlerted) {
            timeToChange -= Time.deltaTime;

            if (timeToChange <= 0) {
                ChangeDirection(false);
            }
            var desiredRotationQuat = Quaternion.Euler(transform.eulerAngles.x, desiredRotation, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQuat, Time.deltaTime * rotationDamping);
            isMoving = false;
            if (randomSeed >= pausePercentage) {
                isMoving = true;
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
            }
        }

        //Animations
        if (isMoving) {
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //collisions with walls causes the object to spin 180 to prevent AI from ramming wall (only when not chasing)
        if ((collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("piller") || collision.gameObject.CompareTag("lightbowl")) && isAlerted == false) {
            ChangeDirection(true);
        }
    }

    private void ChangeDirection(bool isWall) {
        //if it hits wall only allow for rotations between 90-270 degrees
        if (isWall) {
            desiredRotation = transform.eulerAngles.y + 180f;
        } else {
            desiredRotation = Random.Range(0f, 360f);
        }
        timeToChange = 1.5f;
        randomSeed = Random.Range(0, 100);

    }
}