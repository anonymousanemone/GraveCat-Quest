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

    private void quitGame()
    {
        Application.Quit();
    }

    private void MovetoScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //tutorial code from: https://www.youtube.com/watch?v=-7I0slJyi8g&ab_channel=Chris%27Tutorials
    [SerializeField] private int sceneBuildIndex;

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
        print("switching scene to" + sceneBuildIndex);
        sceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
