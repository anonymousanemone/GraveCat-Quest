using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpriteRenderer render;
    private int[] winAmount = { 15, 10, 10, 10, 5 };
    [SerializeField] GameObject winMenu;
    private int curSceneIndex;

    private void Start()
    {
        //loot = GameObject.FindGameObjectWithTag("Loot");
        render = gameObject.GetComponent<SpriteRenderer>();
        if (render)
        {
            render.enabled = false;
        }
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(curSceneIndex);
    }

    //tutorial code from: https://www.youtube.com/watch?v=-7I0slJyi8g&ab_channel=Chris%27Tutorials
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (curSceneIndex == 6 && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelayThenFade());

        }
        else if (render.enabled && other.gameObject.CompareTag("Player"))
        {
            winMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    private void Update()
    {
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(curSceneIndex);
        if (curSceneIndex < 6)
        {
            showLoot();
        }
    }

    private void showLoot()
    {
        if (ScoreManager.instance.getScore() > winAmount[curSceneIndex-1] || Input.GetKeyDown("q"))
        {
            render.enabled = true;
        }
    }

    IEnumerator DelayThenFade()
    {
        yield return new WaitForSeconds(1);
        winMenu.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(curSceneIndex+1);
    }
}
