using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Puntos : MonoBehaviour
{
    public float puntos;
    public Text textoContador;

    private void Update()
    {
        textoContador.text =  "llaves" + puntos.ToString ();
        if (puntos == 4 )
        {
            textoContador.text = "Puerta abierta";
        }
       
    }
}
