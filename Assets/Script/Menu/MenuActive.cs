using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActive : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    void Start()
    {
        MainMenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
