using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private bool isPaused;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if(isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }

    }


    // pause audio too
    // 3ala 7asab audiolistener wala la2
    // AudioListener.pause = true;
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        isPaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        isPaused = false;
    }

}
