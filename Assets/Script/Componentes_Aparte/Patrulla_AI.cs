using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrulla_AI : MonoBehaviour
{
    [Header("Transforms para cordenadas")]
    public Transform[] cordenadas;
    private int destinoCor = 0;
    private NavMeshAgent Enemigo;


        void Start () {
            Enemigo = GetComponent<NavMeshAgent>();

            // Esto hace que nunca para y baje la velocidad entre cordenadas
            Enemigo.autoBraking = false;

            IrAlSiguientePunto();
        }

        //Funcion para que viaje entre cordenadas
        void IrAlSiguientePunto() {
            // si ya no tienen cordenadas se para
            if (cordenadas.Length == 0)
                return;

            // Hace que el Enemigo se diriga a la nueva cordenada
            Enemigo.destination = cordenadas[destinoCor].position;

            // Escoge la siguiente cordenada
            // Cicla todo si es necesario
            destinoCor = (destinoCor + 1) % cordenadas.Length;
        }


        void Update () {
            // Escoge el siguiente destino cuando llega a uno
            if (!Enemigo.pathPending && Enemigo.remainingDistance < 0.5f)
                IrAlSiguientePunto();
        }
}
