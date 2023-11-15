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
    public CanvasGroup LoadButton;
    public CanvasGroup rule;
    public GameObject pause;
    private GameObject _healthManager;
    private GameObject _LevelManager;
    private LevelManagerScript LevelManagerScript_;

    void Start()
    {
        pause = GameObject.Find("PauseWindow");
        if(pause)
            pause.SetActive(false);
        _healthManager = GameObject.FindGameObjectWithTag("HealthManager");
        _LevelManager = GameObject.FindGameObjectWithTag("LevelManager");
        LevelManagerScript_ = _LevelManager.GetComponent<LevelManagerScript>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            pause.SetActive(true);
            Time.timeScale = 0;
        }

    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void About()
    {
        AboutButton.alpha = 0;
        AboutButton.blocksRaycasts = false;
        PlayButton.alpha = 0;
        PlayButton.blocksRaycasts = false;
        QuitButton.alpha = 0;
        QuitButton.blocksRaycasts = false;
        LoadButton.alpha = 0;
        LoadButton.blocksRaycasts = false;
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
        LoadButton.alpha = 1;
        LoadButton.blocksRaycasts = true;
        BackButton.alpha = 0;
        BackButton.blocksRaycasts = false;
        rule.alpha = 0;
        Debug.Log("back");
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    
        //_healthManager.SetActive(false);
        //_LevelManager.SetActive(false);
    }

    public void backtomenu(){
        LevelManagerScript_ = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManagerScript>();
        LevelManagerScript_.save();
        SceneManager.LoadScene(0);
    }

    public void cancel(){
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void load(){
        LevelManagerScript_.load();
        Time.timeScale = 1;
    }
}
