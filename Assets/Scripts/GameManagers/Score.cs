using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] static int[] winAmount = { 15, 10, 10, 10, 5 };

    private int curSceneIndex;
    public static Score instance;


    private void Start()
    {
        instance = this;
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        score = 0;

    }
    private void Update()
    {
        scoreText.text = score.ToString();

        if (score > winAmount[curSceneIndex - 1] || Input.GetKeyDown("q"))
        {
            Loot.instance.ShowLoot();
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

}