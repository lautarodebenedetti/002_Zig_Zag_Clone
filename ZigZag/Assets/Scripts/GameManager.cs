using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score;
    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        this.highScoreText.text = "Best: " + GetHighScore().ToString();
    }
    public void StartGame()
    {
        this.gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
    public void IncreaseScore()
    {
        this.score++;
        this.scoreText.text = this.score.ToString();
        if(this.score > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", this.score);
            this.highScoreText.text = "Best: " + this.score.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.StartGame();
        }
    }
    
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
}
