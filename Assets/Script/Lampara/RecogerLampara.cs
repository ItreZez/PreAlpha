using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerLampara : MonoBehaviour
{
    public GameObject LamparaPlayer;
    public GameObject player;
    public GameObject luz;
    public GameObject shaderOutline;
    private bool tieneLampara;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && Input.GetKey(KeyCode.E))
        {
            shaderOutline.SetActive(false);
            FindObjectOfType<Lampara>().tieneLampara = true;
            luz.SetActive(true);
            LamparaPlayer.SetActive(true);
            Destroy(gameObject);





        }
    }


}
