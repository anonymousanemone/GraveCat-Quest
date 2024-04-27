using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MovetoScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void NextScene()
    {
        //print("switching scene to" + sceneBuildIndex);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }
}
