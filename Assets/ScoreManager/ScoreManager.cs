using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float score = 0.0f;

    private float highScore = 0;

    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore");
    }

    private void updateScoreOnCanvas()
    {
        scoreText.text = score.ToString("0");
    }

    public void addScore(float scoreToAdd = 1.0f)
    {
        score += scoreToAdd;
        updateScoreOnCanvas();
    }

    public void rewriteHighScore(bool updateCanvas = true)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", score);

            if (updateCanvas)
                highScoreText.text = highScore.ToString("00");
        }
    }
}