using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject credits;
    public void playGame()
    {
        SceneManager.LoadScene("2_Intro");
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void HideCredits()
    {
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Spiel wurde beendet!!");
        Application.Quit();
    }
}
