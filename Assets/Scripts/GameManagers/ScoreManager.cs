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

    private void Start()
    {
        score = 0;
        instance = this;
    }
    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public int getScore()
    {
        return score;
    }

}