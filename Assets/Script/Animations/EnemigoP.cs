using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoP : EnemigoPrincipal
{
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stunt();
        Movimiento();
    }

    private void stunt()
    {
        if(isAturdidoPrincipal == true)
        {
            animator.SetBool("ATURDIDO", true);
        }
        if(isAturdidoPrincipal == false) 
        {
            animator.SetBool("ATURDIDO", false);
        }
    }

    private void Movimiento()
    {
        if(pausa == false)
        {
            animator.SetBool("MOVIMIENTO", true);
            
        }
        if(pausa == true)
        {
             animator.SetBool("MOVIMIENTO", false); 
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

    }


}
