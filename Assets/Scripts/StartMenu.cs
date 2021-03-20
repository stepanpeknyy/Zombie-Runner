using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField] Canvas zombieTextCanvas;
    [SerializeField] Canvas helpTextCanvas;

    void Start()
    {
        helpTextCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ShowHelpText()
    {
         helpTextCanvas.enabled = true;
         zombieTextCanvas.enabled = false;
    }

    private void HideHelpText()
    {
         helpTextCanvas.enabled = false;
         zombieTextCanvas.enabled = true;
    }
    public void HelpButton()
    {
        if (helpTextCanvas.enabled == false)
        {
            ShowHelpText();
        }
        else
        {
            HideHelpText();
        }
    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
