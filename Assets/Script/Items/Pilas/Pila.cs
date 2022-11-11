using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila : MonoBehaviour
{
    private Lampara lampara;
    private FP_Controller player;
    public GameObject _pila;
    public GameObject _pilaOutline;

    

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

    private void OnCollisonEnter(Collision other) 
    {
         if(other.gameObject.tag == "Player"  &&  Input.GetMouseButtonDown(0))
         {
            
            lampara.AgregarPilaInventario();
            player.RecogerPila();
            _pilaOutline.SetActive(true);
         }
    }


    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Lampara")_pilaOutline.SetActive(true);
    }

   
 
   private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Lampara")_pilaOutline.SetActive(false);
        
    }
}
