using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chiave : MonoBehaviour
{
    public GameObject tasto;
    public GameObject chiave3d;
    public GameObject chiave2d;
    public GameObject hitbox;
    public GameObject consumabile;

    private void Start()
    {
        if(!NewBehaviourScript.chiave3d)
        {
            chiave3d.SetActive(false);
            //chiave2d.SetActive(true);
            hitbox.SetActive(false);
            tasto.SetActive(false);
        }
        else
        {
            chiave3d.SetActive(true);
            hitbox.SetActive(true);
        }
        if(!NewBehaviourScript.chiave2d)
        {
            chiave2d.SetActive(false);
        }
        else
        {
            chiave2d.SetActive(true);
        }
        if(!NewBehaviourScript.consumabile)
        {
            consumabile.SetActive(false);
        }
        else
        {
            consumabile.SetActive(true);
        }
    }

    public void ChiaveTasto()
    {
        NewBehaviourScript.chiave3d = false;
        NewBehaviourScript.chiave2d = true;
        chiave3d.SetActive(false);
        chiave2d.SetActive(true);
        hitbox.SetActive(false);
        tasto.SetActive(false);
    }
}
