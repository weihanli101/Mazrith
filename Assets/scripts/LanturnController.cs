using System.Collections.Generic;
using UnityEngine;

public class LanturnController : MonoBehaviour {

    public new Light light;
    public float minIntensity = 3f;
    public float maxIntensity;
    public int smoothing = 5;

    public GameObject player;

    Queue<float> smoothQueue;
    float lastSum = 0;


    public void Reset() {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start() {
        smoothQueue = new Queue<float>(smoothing);
        light = GetComponent<Light>();
    }

    void Update() {
        if (light == null)
            return;
        //get the max intensity from the CharacterControllerScript
        CharacterController characterControllerScript = player.GetComponent<CharacterController>();
        light.range = characterControllerScript.playerMaxLightIntensity;
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
    }
}
