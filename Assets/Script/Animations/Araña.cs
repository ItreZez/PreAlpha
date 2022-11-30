using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Araña : EnemyState
{
    public Animator animator;
    private VidaEnemigo muerto;
    private ChecarHit hit;



    void Start()
    {
        muerto = GetComponent<VidaEnemigo>();
        hit = GetComponent<ChecarHit>();
       


    }

    // Update is called once per frame
    void Update()
    {
        Stunt();
        Die();
        Attack();
       



    }

    private void Stunt()
    {

        if (isAturdido == true)
        {
            animator.SetBool("STUNT", true);
        }
        if (isAturdido == false)
        {
            animator.SetBool("STUNT", false);
        }
    }

    private void Die()
    {
        if (muerto.vida == 0)
        {
            animator.SetBool("MUERTE", true);
        }
        if (muerto.vida < 0)
        {
            animator.SetBool("MUERTE", false);
        }

    }

    private void Attack()
    {
        if(hit.attack == true)
        {
            animator.SetBool("ATTACK",true);
        }
        else
        {
            animator.SetBool("ATTACK",false);
        }
        
    }


}
