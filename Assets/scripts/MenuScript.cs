using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Help() {
        SceneManager.LoadScene(2);
    }

    public void ReturnToMain() {
        SceneManager.LoadScene(0);
    }

    public void Credits() {
        SceneManager.LoadScene(3);
    }
    
}
