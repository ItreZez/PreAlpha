using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila : MonoBehaviour
{
    private Lampara lampara;
    private FP_Controller player;
    public GameObject _pila;

    

    // Start is called before the first frame update
    void Start()
    {
        lampara = GetComponent<Lampara>();
        player = GetComponent<FP_Controller>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
         if(Input.GetMouseButtonDown(0))
         {
             _pila.SetActive(false);
            lampara.AgregarPilaInventario();
            player.RecogerPila();
         }

        
        
    }
}
