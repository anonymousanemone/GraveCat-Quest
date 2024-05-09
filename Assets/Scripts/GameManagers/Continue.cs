using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Continue : MonoBehaviour
{
    private int sceneToContinue;
    [SerializeField] TextMeshProUGUI buttonText;
    private void Awake()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
    }

    private void Update()
    {

        if (sceneToContinue != 0)
        {
            buttonText.text = "Resume";
        }
    }
    public void ContinueGame(){
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if(sceneToContinue != 0){
            SceneManager.LoadScene(sceneToContinue);
        }
        else
            SceneManager.LoadScene(1); 
    }
}
