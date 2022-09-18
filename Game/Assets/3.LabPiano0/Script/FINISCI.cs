using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FINISCI : MonoBehaviour
{
    public GameObject portaAudio;
    public GameObject maincamera;
    public GameObject camerafine;
    private void OnTriggerEnter(Collider other)
    {
        NewBehaviourScript.FINE = true;
        AudioSource a = portaAudio.GetComponent<AudioSource>();
        camerafine.SetActive(true);
        maincamera.SetActive(false);
        a.Play();
    }
}
