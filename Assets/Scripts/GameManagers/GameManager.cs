using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=_cR_rRkrFy4&t=79s&ab_channel=ThegamedevTraum
    private void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //tutorial code from: https://www.youtube.com/watch?v=-7I0slJyi8g&ab_channel=Chris%27Tutorials
    private void OnTriggerEnter2D(Collider2D other)
    {
        //sceneBuildIndex = 1;
        if (other.gameObject.CompareTag("Player"))
        {
            nextScene();
        }
    }

    private void nextScene()
    {
        //print("switching scene to" + sceneBuildIndex);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause();
        }
        winCondition();
    }
    private void gamePause()
    {
        
    }
    private int[] winAmount = { 15, 10, 5, 10, 10 };
    private void winCondition()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (ScoreManager.instance.getScore() > winAmount[curSceneIndex-1] || Input.GetKeyDown("q"))
        {
            revealLoot();
            Debug.Log("hello");

        }
    }

    private void revealLoot()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Loot");
        item.SetActive(true);
    }
}
