using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarHit : MonoBehaviour
{
    public Collider colliderEnemy;
    public GameObject player;
    public bool attack;
    


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            attack = true;
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2);
        attack = false;
    }
}
