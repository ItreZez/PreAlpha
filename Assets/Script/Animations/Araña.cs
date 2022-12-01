using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Araña : EnemyState
{
    public Animator animator;
    private VidaEnemigo muerto;
    public bool reproduciendo = false;

   

    private void Start()
    {
        muerto = GetComponent<VidaEnemigo>();

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

            if(reproduciendo == false)
            {
                reproduciendo = true;
                Destroy(Instantiate(AS_Die, transform.position, Quaternion.identity), 1f);
            }
            
            
            
        }
        if (muerto.vida < 0)
        {
            animator.SetBool("MUERTE", false);
        }

    }

    private void Attack()
    {
        if(Ataque == true)
        {
            animator.SetBool("ATTACK", true);

        }
        if (Ataque == false)
        {
            animator.SetBool("ATTACK",false);
        }
        
    }

}
