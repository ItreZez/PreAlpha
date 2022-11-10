using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapa : MonoBehaviour
{
    [SerializeField] private GameObject MapaUI;
    public bool MapaA;
    public bool MapaAct;
    private void Start()
    {
        MapaUI.SetActive(false);
        MapaA = FindObjectOfType<FP_Controller>().recogioMapa;
    }

   
    private void Update()
    {
        MapaA = FindObjectOfType<FP_Controller>().recogioMapa;

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
}
