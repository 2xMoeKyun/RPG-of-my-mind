using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject control;
    public void ShowControl()
    {
        control.SetActive(true);
    }

    public void HideControl()
    {
        control.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
