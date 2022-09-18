using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livello3Condizioni : MonoBehaviour
{
    public GameObject missione;
    public GameObject errore;
    public GameObject erroreChiave;
    public GameObject chiusura;

    private void OnTriggerEnter(Collider other)
    {
        if (NewBehaviourScript.numeroStelleLivello1 > 0 && NewBehaviourScript.numeroStelleLivello2 > 0)
        {
            if (!NewBehaviourScript.chiave3d)
            {
                missione.SetActive(true);
            }
            else
            {
                erroreChiave.SetActive(true);
            }
        }
        else
        {
            errore.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (NewBehaviourScript.numeroStelleLivello1 > 0 && NewBehaviourScript.numeroStelleLivello2 > 0)
        {
            if (!NewBehaviourScript.chiave3d)
            {
                missione.SetActive(false);
            }
            else
            {
                erroreChiave.SetActive(false);
            }
        }
        else
        {
            errore.SetActive(false);
        }
        AudioSource a = chiusura.GetComponent<AudioSource>();
        a.Play();
    }
}
