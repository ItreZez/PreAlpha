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
}//END class MainMenu


