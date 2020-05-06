using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void enterBattle()
    {
        SceneManager.LoadScene(2);
    }

    public void lostBattle()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("Game is now closed");
        Application.Quit();
    }

}
