using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaState : MonoBehaviour
{
    [SerializeField] private bool Pausa;
    [SerializeField] private GameObject MenuPausa;

    private void Start()
    {
       Pausa = false;
       MenuPausa.SetActive(false);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Pausa)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        MenuPausa.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Pausa = true;

    }

    void Resume()
    {
        MenuPausa.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Pausa = false;

    }
}
