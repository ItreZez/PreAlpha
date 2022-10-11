using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CalidadDeImagen : MonoBehaviour
{//START CLASS CalidadDeImagen

    public TMP_Dropdown dropdown;

    public int calidad;

    private void Start()
    {
        calidad = PlayerPrefs.GetInt("numero de calidad", 4);
        dropdown.value = calidad;
        AjustarCalidad();

    }

    public void AjustarCalidad ()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numero de calidad", dropdown.value);
        calidad = dropdown.value;
    }
}//END CLASS CalidadDeImagen
