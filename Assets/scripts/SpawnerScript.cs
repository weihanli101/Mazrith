using System;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
    public float orbSpawnTime;
    public GameObject lightOrb;

    private Boolean isOrbSpawned;
    private GameObject lightOrbClone;
    // Use this for initialization
    void Start() {
        isOrbSpawned = false;
        SpawnOrb();
        InvokeRepeating("SpawnOrb", orbSpawnTime, orbSpawnTime);
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("lightorb")) {
            isOrbSpawned = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        isOrbSpawned = false;
    }

    private void SpawnOrb() {
        if (isOrbSpawned == false) {
            Instantiate(lightOrb, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }
    }
}
