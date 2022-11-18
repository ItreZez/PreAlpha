using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UITextos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Texto;
    [SerializeField] private string TextoString;


    [SerializeField] private TextMeshProUGUI TextoMapa;
    [SerializeField] private string TextoMapaString;

    [Header("bools")]
    [SerializeField] private bool TieneLampara;
    [SerializeField] private bool TieneMapa;
    [SerializeField] private bool MapaAct;

    private void Start()
    {
        Texto.text = TextoString;
        TextoString = "";

        TextoMapa.text = TextoMapaString;
        TextoMapaString = "";

        TieneLampara = FindObjectOfType<Lampara>().tieneLampara;
        TieneMapa = FindObjectOfType<UIMapa>().MapaA;

        MapaAct = FindObjectOfType<UIMapa>().MapaAct;
    }
    private void Update()
    {
        Texto.text = TextoString;

        TextoMapa.text = TextoMapaString;

        TieneLampara = FindObjectOfType<Lampara>().tieneLampara;


        TieneMapa = FindObjectOfType<UIMapa>().MapaA;
        MapaAct = FindObjectOfType<UIMapa>().MapaAct;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Text_Recoger" && TieneLampara == false)
        {
            TextoString = "Pick up items with E";
        }

        if(other.gameObject.tag == "Text_Linterna1" && TieneLampara == true)
        {
            TextoString = "Turn the flashlight on and off with RIGHT CLICK";
        }

        if (other.gameObject.tag == "Text_Linterna2" && TieneLampara == true)
        {
            TextoString = "Take the batteries and recharge with R when the battery runs out";
        }

        if (other.gameObject.tag == "Text_Mov2")
        {
            TextoString = "Q to crouch, SHIFT to run, and SPACE to jump";
        }

        if (other.gameObject.tag == "Text_Ataque")
        {
            TextoString = "Use the flashlight to stun enemies";
        }

        if (other.gameObject.tag == "Text_Ataque2")
        {
            TextoString = "Every time you stun them the enemies lose life";
        }

        if (other.gameObject.tag == "Text_Mapa" && TieneMapa == true && MapaAct == false)
        {
            TextoMapaString = "See the map with M";
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Text_Recoger" && TieneLampara == true)
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Linterna1")
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Linterna2")
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Mov2")
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Ataque")
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Ataque2")
        {
            TextoString = "";
        }

        if (other.gameObject.tag == "Text_Mapa")
        {
            TextoMapaString = "";
        }
        if (other.gameObject.tag == "Text_Mapa" )
        {
            TextoMapaString = "";
        }
    }
}
