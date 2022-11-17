using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextosUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Textosui;
    [SerializeField] private string TexValue;


    private void Start()
    {
        Textosui.text = TexValue;
        TexValue = "";
    }

    private void Update()
    {
        Textosui.text = TexValue;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CartelLampara")
        {
            TexValue = "CLick derecho para activar y desactivar la lampara";
        }
        
        if (other.gameObject.tag == "LamparaRecoger")
        {
            TexValue = "Recoge objetos con E";
        }
        if (other.gameObject.tag == "MovSecundario")
        {
            TexValue = "Corre shift, agacharse CTRL, salto Space";
        }
        if (other.gameObject.tag == "TextoAtaque")
        {
            TexValue = "Ataca a los enemigos con la linterna";
        }
        if (other.gameObject.tag == "UIMapa")
        {
            TexValue = "Abre el Mapa con M";
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CartelLampara")
        {
            TexValue = "";
        }
        if (other.gameObject.tag == "LamparaRecoger")
        {
            TexValue = "";
        }
        if (other.gameObject.tag == "MovSecundario")
        {
            TexValue = "";
        }
        if (other.gameObject.tag == "TextoAtaque")
        {
            TexValue = "";
        }
        if (other.gameObject.tag == "UIMapa")
        {
            TexValue = "";
        }
    }

}


