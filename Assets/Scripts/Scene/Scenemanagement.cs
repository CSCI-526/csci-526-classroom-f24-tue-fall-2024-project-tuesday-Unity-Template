using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanagement : MonoBehaviour
{
    public void LoadStart()
    {
        Time.timeScale = 1f;
        PlayerResource.Instance.ResteData();
        SceneManager.LoadScene(0);
    }
    public void LoadTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void LoadSafehouse()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void LoadLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }

    public void LoadLevel4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }

    public void onQuitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
