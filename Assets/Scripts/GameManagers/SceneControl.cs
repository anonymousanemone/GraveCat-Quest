using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject playScreen;

    private static int homeSceneIndex = 0;
    private int curSceneIndex;

    private bool paused;

    public static SceneControl instance;


    private void Start()
    {
        instance = this;
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void WinGame()
    {
        GameObject.Find("/Plague Crow").SetActive(false);
        GameObject.Find("/BirdSpawner").SetActive(false);
        StartCoroutine(winning(1f));
    }
    IEnumerator winning(float delay)
    {
        yield return new WaitForSeconds(delay);
        playScreen.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0;

    }

    public void LoseGame()
    {
        GameObject.Find("/BirdSpawner").SetActive(false);
        StartCoroutine(losing(1.5f));
    }
    IEnumerator losing(float delay)
    {
        yield return new WaitForSeconds(delay);
        playScreen.SetActive(false);
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }


    /** Scene changes **/

    public static void NextScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = curSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        Time.timeScale = 1f;
    }

    public static void MenuScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", curSceneIndex);
        SceneManager.LoadScene(homeSceneIndex);
        Time.timeScale = 1f;
    }

    public static void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1f;
    }


    /** Game options **/

    public void TogglePause()
    {
        if (!paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }

    }

    public void Restart()
    {
        //https://www.youtube.com/watch?v=_cR_rRkrFy4&t=79s&ab_channel=ThegamedevTraum
        SceneManager.LoadScene(curSceneIndex);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        paused = true;
        instance.pauseScreen.SetActive(paused);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        paused = false;
        instance.pauseScreen.SetActive(paused);
        Time.timeScale = 1f;
    }

    public static void ShowScreen(GameObject screen)
    {
        screen.SetActive(true);
        Time.timeScale = 0f;
    }

}
