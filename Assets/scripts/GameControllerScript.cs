using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
    //game balancing values
    public float scoreAddingFreqency;
    public GameObject gameOverMenu;
    public GameObject player;
 
    //UI
    public Text scoreText;
    public Slider healthBar;
    public GameObject joystick;
    public Text highScoreText;
    public Text finalScoreText;

    private int score;
    private int highScore;
	// Use this for initialization
	void Start () {
        score = 0;
        InvokeRepeating("AddScore", scoreAddingFreqency, scoreAddingFreqency);
	}

	// Update is called once per frame
	void Update () {
        //game over
        if(player.GetComponent<CharacterController>().health <= 0) {
            CancelInvoke();
            endGame();
        } else {
            UpdateUI();
        }

    }

    private void endGame() {
        //show gameover menu
        gameOverMenu.SetActive(true);

        //hide other ui items
        scoreText.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        joystick.SetActive(false);

        //set final score text
        if (PlayerPrefs.HasKey("highestScore")) {
            highScore = PlayerPrefs.GetInt("highestScore");

            //set new highscore if greater
            if(score > highScore) {
                highScore = score;
                PlayerPrefs.SetInt("highestScore",highScore);
            }
        }
        else {
            highScore = score;
        }
        finalScoreText.text = "Score: " + score.ToString();
        highScoreText.text = "Best: " + highScore.ToString();
    }

    public void onRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void onExit() {
        SceneManager.LoadScene(0);
    }

    private void AddScore() {
        score += 1;
    }
    
    private void UpdateUI() {
        scoreText.text = "Score: " + score.ToString();
    }
}
