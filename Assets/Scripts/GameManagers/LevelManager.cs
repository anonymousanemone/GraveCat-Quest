using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int curSceneIndex;
    private int score;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] static int[] winAmount = { 15, 10, 10, 10, 5 };

    [SerializeField] private GameObject loot;
    private SpriteRenderer render;
    public static LevelManager instance;
    

    private void Start()
    {
        instance = this;
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        score = 0;

        //loot = GameObject.FindWithTag("Loot");
        render = loot.GetComponent<SpriteRenderer>();
        render.enabled = false;
    }
    private void Update()
    {
        scoreText.text = score.ToString();

        //curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(curSceneIndex);

        showLoot();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public int getScore()
    {
        return score;
    }

    //tutorial code from: https://www.youtube.com/watch?v=-7I0slJyi8g&ab_channel=Chris%27Tutorials
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (render.enabled && other.gameObject.CompareTag("Player"))
        {
            SceneControl.ShowScreen(SceneControl.instance.winScreen);
            SceneControl.instance.playScreen.SetActive(false);
        }

    }

    private void showLoot()
    {
        if (score > winAmount[curSceneIndex - 1] || Input.GetKeyDown("q"))
        {
            render.enabled = true;
        }
    }



}