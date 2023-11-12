using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PauseWindow : MonoBehaviour
{
   
    public GameObject pauseWindow;
    private bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        pauseWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPause = !isPause;

            if(isPause == true)
            {
                pauseWindow.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                pauseWindow.SetActive(false);
                Time.timeScale = 1;
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    
    }

    public void Quit(){
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}
