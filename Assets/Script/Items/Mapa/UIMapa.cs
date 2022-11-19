using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapa : MonoBehaviour
{
    [SerializeField] private GameObject MapaUI;
    [SerializeField] private GameObject TexMapaAbrir;


    public bool MapaA;
    public bool MapaAct;

    public bool AbrioMapa;

    private void Start()
    {
        MapaUI.SetActive(false);
        MapaA = FindObjectOfType<FP_Controller>().recogioMapa;
        AbrioMapa = false;
        
    }

   
    private void Update()
    {
        MapaA = FindObjectOfType<FP_Controller>().recogioMapa;

        abrioMapaText();

        if (MapaA == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if(MapaAct == true)
                {
                    MapaUIF();
                }
                else
                {
                    MapaUIV();
                }
            }
        }
    }


    void MapaUIV()
    {
        MapaUI.SetActive(true);
        MapaAct = true;
    }

    void MapaUIF()
    {
        MapaUI.SetActive(false);
        MapaAct = false; 
    } 

    void abrioMapaText()
    {
        if(MapaA ==  true && AbrioMapa == false && MapaAct == false)
        {
            TexMapaAbrir.SetActive(true);
            AbrioMapa = true;
        }
        if(AbrioMapa ==  true && MapaAct == true)
        {
            TexMapaAbrir.SetActive(false);
        }

    }
}
