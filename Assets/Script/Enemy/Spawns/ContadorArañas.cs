using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorArañas : MonoBehaviour
{
    public int arañasEnMapa ;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    public FP_Controller contenedor_De_Llaves;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReiniciarContador());
    }

    // Update is called once per frame
    void Update()
    {
        if(contenedor_De_Llaves.contadorLlaves == 5)
        {
            Destroy(spawn1);
            Destroy(spawn2);
            Destroy(spawn3);
            Destroy(spawn4);
        }
    }

   IEnumerator ReiniciarContador()
   {
    yield return new WaitForSeconds(70f);
    arañasEnMapa = arañasEnMapa - 10;
    StartCoroutine(ReiniciarContador());

   }
}

