using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    public bool hit;
    public bool aturdido;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(hit == false && aturdido == false)
            {
                StartCoroutine(FindObjectOfType<FP_Controller>().DanoPlayer());
                hit = true;
                Invoke("attackReset",5f);

            }   
        }

        if (other.gameObject.tag == "Lampara")
        {
            aturdido = true;
            Invoke("aturdidoReset" , 3f);

        }
    }

    void attackReset()
    {
        hit = false;
    }

    void aturdidoReset()
    {
        aturdido = false;
    }
}
