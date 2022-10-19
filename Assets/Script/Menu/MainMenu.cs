using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{//START class MainMenu 

    public void jugar()
    {

        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Debug.Log("Salir'...");
        Application.Quit();

    }

    public void GameOver()
    {

        SceneManager.LoadScene(2);
    }

    public void MainMenuBack()
    {

        SceneManager.LoadScene("MainMenu");
    }
    public void TryAgain()
    {

        SceneManager.LoadScene("PreAlpha");
    }


}//END class MainMenu


