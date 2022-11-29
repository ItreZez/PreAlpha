using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject RecogerIteamSF;


    private void OnTriggerStay(Collider other)
    {

        if (/*ther.gameObject == player*/other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Destroy(Instantiate(RecogerIteamSF, player.transform.position, Quaternion.identity), 1f);

            Debug.Log("Recogio Mapa");
            FindObjectOfType<FP_Controller>().recogioMapa = true;
            Destroy(gameObject);
            FindObjectOfType<FP_Controller>().UIllave.SetActive(true);
            FindObjectOfType<FP_Controller>().UIMapa.SetActive(true);


        }
    }
}
