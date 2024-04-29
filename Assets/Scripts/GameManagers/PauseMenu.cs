using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; 
    private int currentSceneIndex; 

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
    }    

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int sceneID){
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex); 
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
