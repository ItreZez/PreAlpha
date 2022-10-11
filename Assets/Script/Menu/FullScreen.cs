using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//PARA RESOLUCION
using TMPro;

public class FullScreen : MonoBehaviour
{//START CLASS FullScreen
    public Toggle toggle;


    //PARA RESOLUCION
    public TMP_Dropdown resolucionesDropdown;
    Resolution[] resoluciones;


    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i <resoluciones.Length; i++)
        {

            string opcion = resoluciones[i].width +  "x" +resoluciones[i].height;
            opciones.Add(opcion);


            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }

        resolucionesDropdown.AddOptions(opciones);
        resolucionesDropdown.value = resolucionActual;
        resolucionesDropdown.RefreshShownValue();


    }

    public void CambiarResolucion(int indiceResolucion)
    {

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

}//END CLASS FullScreen
