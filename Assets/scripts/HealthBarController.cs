using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
    public GameObject player;

    private CharacterController CharacterController;
    private Slider silder;
	// Use this for initialization
	void Start () {
        CharacterController = player.GetComponent<CharacterController>();
        silder = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        silder.value = CharacterController.health;

    }
}
