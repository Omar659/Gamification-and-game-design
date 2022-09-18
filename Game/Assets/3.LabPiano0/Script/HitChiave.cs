using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChiave : MonoBehaviour
{
    public GameObject tasto;
    private void OnTriggerEnter(Collider other)
    {
        tasto.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tasto.SetActive(false);
    }
}
