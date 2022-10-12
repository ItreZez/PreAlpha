using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavPoints : MonoBehaviour
{

    private NavMeshAgent nma = null;
    private Bounds bndFloor;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject pole;
    [SerializeField] private float BoundChangeTime;

    private Vector3 moveto;

    private void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        bndFloor = floor.GetComponent<Renderer>().bounds;

        SetRandomDestination();
    }

    void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);

        moveto = new Vector3(rx, transform.position.y, rz);

        nma.SetDestination(moveto);

        pole.transform.position = new Vector3(moveto.x, this.transform.position.y, moveto.z);

        Invoke("CheckPointOnPath", BoundChangeTime);


    }

    void CheckPointOnPath()
    {
        if (nma.pathEndPosition != moveto)
        {
            //el punto no esta en el navMash
            SetRandomDestination();

        }
    }


}
