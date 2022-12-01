using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoP : EnemigoPrincipal
{
    [SerializeField] private Animator animator;

    private bool stuntAnim;

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
        if (isAturdidoPrincipal == true)
        {
            stuntAnim = true;
            if (stuntAnim == true)
            {
                stuntAnim = false;
                animator.SetBool("ATURDIDO", true);
            }

        }
        if (isAturdidoPrincipal == false)
        {
            animator.SetBool("ATURDIDO", false);
        }
    }

    private void Movimiento()
    {
        if (pausa == false)
        {
            animator.SetBool("MOVIMIENTO", true);
            animator.SetBool("IDLE", false);

        }
        if (pausa == true)
        {
            animator.SetBool("MOVIMIENTO", false);
            animator.SetBool("IDLE", true);
        }
    }


   




}
