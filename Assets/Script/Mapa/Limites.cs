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
        IsTrigger = LimiteTutorial.GetComponent<BoxCollider>().isTrigger;

        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
    }
    void Update()
    {
        LimiteTutorial.GetComponent<BoxCollider>().isTrigger = IsTrigger;

        GameObject.Find("Player").GetComponent<FP_Controller>().Escondido = Escondido;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Limit_Casa")
        {
            Escondido = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Limit_Tutorial")
        {
            IsTrigger = false;
        }
        if (other.tag == "Limit_Casa")
        {
            Escondido = false;
        }
    }
}
