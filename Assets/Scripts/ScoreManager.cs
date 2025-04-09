using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update when called by Bullet.cs
    public void AddScore(){
        score += 100;
        scoreText.text = "Score: " + score.ToString();
        
    }
    public void SaveScore(){
        if (highScore < score){
            PlayerPrefs.SetInt("highscore", score);
            highScoreText.text = "High Score: " + score.ToString();
        }
    }
    
}
