using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sceneloader : MonoBehaviour
{
    public void LoadScene1Ours()
    {
        SceneManager.LoadScene("Scene1 Ours");
    }

    public void LoadScene1mlagents()
    {
        SceneManager.LoadScene("MagicianVsMagici _ Playn 1");
    }
    public void LoadScene2mlagents()
    {
        SceneManager.LoadScene("ArcherVsArcher _ Play");
    }
    public void LoadScene3mlagents()
    {
        SceneManager.LoadScene("BerserkerVsBerserker _ Play");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTraining()
    {
        SceneManager.LoadScene("TrainScene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
