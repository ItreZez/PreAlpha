using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Llaves : MonoBehaviour
{
    [SerializeField] private GameObject outline;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lampara") outline.SetActive(true);
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lampara") outline.SetActive(false);

    }
}
