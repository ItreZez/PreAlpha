using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorArañas : MonoBehaviour
{
    public int arañasEnMapa ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReiniciarContador());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator ReiniciarContador()
   {
    yield return new WaitForSeconds(70f);
    arañasEnMapa = arañasEnMapa - 10;
    StartCoroutine(ReiniciarContador());

   }
}

