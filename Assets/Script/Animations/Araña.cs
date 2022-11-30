using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Araña : EnemyState
{
    public Animator animator;
    private VidaEnemigo muerto;

    public FP_Controller hit;

    void Start()
    {
        muerto = GetComponent<VidaEnemigo>();
        hit = GetComponent<FP_Controller>();


    }

    // Update is called once per frame
    void Update()
    {
        Stunt();
        Die();
       
       



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
        animator.SetInteger("ATTACK", (int)currentAIState);
    }


}
