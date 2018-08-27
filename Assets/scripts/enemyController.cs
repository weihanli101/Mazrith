using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int moveSpeed;
    public float rotationDamping;
    [Range(0, 100)]
    public float pausePercentage;
    public float alertRadius;
    public float alertedMovementSpeed;

    private float timeToChange;
    private float desiredRotation;
    private float randomSeed;
    private bool isAlerted;

    void Start() {
        ChangeDirection(false);
        randomSeed = Random.Range(0, 100);
    }

    void Update() {
        foreach(Collider collider in Physics.OverlapSphere(transform.position, alertRadius)) {
            if(collider.CompareTag("Player")) {
                isAlerted = true;
                Vector3 forceDirection = collider.transform.position - transform.position;
                transform.position += forceDirection.normalized * alertedMovementSpeed * Time.deltaTime;
            } else {
                isAlerted = false;
            }
        }
        // roaming mode when AI is not alerted
        if(!isAlerted) {
            timeToChange -= Time.deltaTime;

            if (timeToChange <= 0) {
                ChangeDirection(false);
            }
            var desiredRotationQuat = Quaternion.Euler(transform.eulerAngles.x, desiredRotation, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQuat, Time.deltaTime * rotationDamping);
            if (randomSeed <= pausePercentage) {
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
            }
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