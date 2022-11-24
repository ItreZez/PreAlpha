using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour
{
    [SerializeField] private bool IsTrigger;
    [SerializeField] private GameObject LimiteTutorial;

    void Start()
    {
        IsTrigger = LimiteTutorial.GetComponent<BoxCollider>().isTrigger;
    }
    void Update()
    {
        LimiteTutorial.GetComponent<BoxCollider>().isTrigger = IsTrigger;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Limit_Tutorial")
        {
            IsTrigger = false;
        }
    }
}
