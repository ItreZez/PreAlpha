using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Contenedor_de_Llaves : MonoBehaviour
{
    //variables
    [Header("Parte de Llave")]
    public GameObject[] llaves;

    //hacer referencia al escript del player para sumar el contador de llaves
    FP_Controller Player_Controller;
    EnemigoPrincipal enemigoPrincipal;

    public bool recogioLlave;


    //public GameObject ObjPuntos; ACTIVAR OTRA VEZ *--------------------------------------------------------------
    public float numeroDeLlaves;



    private void Start()
    {
        //Llamar al script del player
        Player_Controller = FindObjectOfType<FP_Controller>();
        enemigoPrincipal = GetComponent<EnemigoPrincipal>();
    }

    private void Update()
    {
        if (Player_Controller.nombreLlave.Contains("Llave") && Player_Controller.siRango == true)
        {
            RecogerLlave(Player_Controller.nombreLlave);
        }
        
        
    }
    //tags de llave 1 llave 2 llave 3 llave 4
    public void RecogerLlave(string tag)
    {

        //Para poder recoger el item/llave y agregar 1+ al contador de llaves
        if (Input.GetMouseButtonDown(0))
        {
           // ObjPuntos.GetComponent<Puntos>().puntos += numeroDeLlaves; *-------------------ACTIVAR OTRA VESZ*-*-*-*-*--*-*-*-*-*-*-*

            for (int i = 0; i < llaves.Length; i++)
            {

                if (llaves[i].tag == tag)
                {
                    recogioLlave = true;
                    llaves[i].SetActive(false);
                    Player_Controller.siRango = false;
                    Player_Controller.nombreLlave = "NoRango";
                    Player_Controller.contadorLlaves++;
                    Debug.Log("llave recogida " + Player_Controller.contadorLlaves);
                    recogioLlave = false;
                    
            
                }
                else
                {
                    Debug.Log("No estas en Rango");
                }

            }



        }
    }



   









}
