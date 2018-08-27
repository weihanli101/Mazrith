using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int moveSpeed;
    public float rotationDamping;
    public float pausePercentage;
    private float timeToChange;
    private float desiredRotation;
    private float randomSeed;

    // Use this for initialization
    void Start() {
        ChangeDirection(false);
        randomSeed = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update() {
        timeToChange -= Time.deltaTime;
        
        if(timeToChange <= 0) {
            ChangeDirection(false);
        }
        var desiredRotationQuat = Quaternion.Euler(transform.eulerAngles.x, desiredRotation, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQuat, Time.deltaTime * rotationDamping);
        if(randomSeed <= pausePercentage) {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("piller") || collision.gameObject.CompareTag("lightbowl")) {
            ChangeDirection(true);
        }
    }

    private void ChangeDirection(bool isWall) {
        //if it hits wall only allow for rotations between 90-270 degrees
        if (isWall) {
            desiredRotation = desiredRotation + 180f;
        } else {
            desiredRotation = Random.Range(0f, 360f);
        }
        timeToChange = 1.5f;
        randomSeed = Random.Range(0, 100);

    }
}