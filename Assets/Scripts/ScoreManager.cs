using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    //public Text scoreText;
    public TextMeshProUGUI scoreText;
    private int score;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Start()
    {
        score = 0;
        //UpdateScoreText();
    }
    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        //UpdateScoreText();
    }

    //private void UpdateScoreText()
    //{
    //    if (scoreText != null)
    //    {
    //        scoreText.text = "Score: " + score;
    //    }
    //}
}