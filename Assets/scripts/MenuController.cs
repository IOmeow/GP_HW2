using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    
    public CanvasGroup AboutButton;
    public CanvasGroup PlayButton;
    public CanvasGroup BackButton;
    public CanvasGroup QuitButton;
    public CanvasGroup rule;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void About()
    {
        AboutButton.alpha = 0;
        AboutButton.blocksRaycasts = false;
        PlayButton.alpha = 0;
        PlayButton.blocksRaycasts = false;
        QuitButton.alpha = 0;
        QuitButton.blocksRaycasts = false;
        BackButton.alpha = 1;
        BackButton.blocksRaycasts = true;
        rule.alpha = 1;
    }

    public void Back()
    {
        PlayButton.alpha = 1;
        PlayButton.blocksRaycasts = true;
        AboutButton.alpha = 1;
        AboutButton.blocksRaycasts = true;
        QuitButton.alpha = 1;
        QuitButton.blocksRaycasts = true;
        BackButton.alpha = 0;
        BackButton.blocksRaycasts = false;
        rule.alpha = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
