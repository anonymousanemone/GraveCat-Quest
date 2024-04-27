using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        //winCondition();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public int getScore()
    {
        return score;
    }

    //private int[] winAmount = { 15, 10, 5, 10, 10 };
    //private void winCondition()
    //{
    //    int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    if (ScoreManager.instance.getScore() > winAmount[curSceneIndex - 1] || Input.GetKeyDown("q"))
    //    {
    //        revealLoot();
    //        Debug.Log("hello");

    //    }
    //}

    //private void revealLoot()
    //{
    //    GameObject item = GameObject.FindGameObjectWithTag("Loot");
    //    item.SetActive(true);
    //}
}