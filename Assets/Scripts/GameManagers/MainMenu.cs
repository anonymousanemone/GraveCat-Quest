using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    private int sceneToContinue;

    [SerializeField] int mapSceneID;
    [SerializeField] GameObject creditScreen;
    [SerializeField] GameObject instructionScreen;
    [SerializeField] GameObject mainScreen;

    private void Start()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if (sceneToContinue != 0)
        {
            buttonText.text = "Resume";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BacktoMain();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DoGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != 0)
        {
            SceneControl.MoveToScene(sceneToContinue);
        }
        else
            SceneControl.MoveToScene(1);
    }

    public void GotoMap()
    {
        SceneManager.LoadScene(mapSceneID);
    }

    public void ShowCredits()
    {
        creditScreen.SetActive(true);
        mainScreen.SetActive(false);

    }

    public void ShowInstructions()
    {
        instructionScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void BacktoMain()
    {
        instructionScreen.SetActive(false);
        creditScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
}
