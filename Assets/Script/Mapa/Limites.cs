using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour
{
    [SerializeField] private bool IsTrigger;
    [SerializeField] private GameObject LimiteTutorial;
    [SerializeField] private bool Escondido;

    void Start()
    {
        IsTrigger = LimiteTutorial.GetComponent<BoxCollider>().isTrigger = true;

        //Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
    }
    void Update()
    {
        LimiteTutorial.GetComponent<BoxCollider>().isTrigger = IsTrigger;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Limit_Casa")
        {
            GameObject.Find("Player").GetComponent<FP_Controller>().Escondido = true;        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Limit_Tutorial")
        {
            IsTrigger = false;
        }
        if (other.tag == "Limit_Casa")
        {
            GameObject.Find("Player").GetComponent<FP_Controller>().Escondido = false;
        }
    }
}
