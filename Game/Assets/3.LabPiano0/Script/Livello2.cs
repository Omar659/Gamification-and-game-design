using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livello2 : MonoBehaviour
{
    public GameObject missione;
    public GameObject errore;
    public GameObject chiusura;

    private void OnTriggerEnter(Collider other)
    {
        if (NewBehaviourScript.numeroStelleLivello1 > 0)
        {
            missione.SetActive(true);
        }
        else
        {
            errore.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (NewBehaviourScript.numeroStelleLivello1 > 0)
        {
            missione.SetActive(false);
        }
        else
        {
            errore.SetActive(false);
        }
        AudioSource a = chiusura.GetComponent<AudioSource>();
        a.Play();
    }
}
