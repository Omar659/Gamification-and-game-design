using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisattivaAncoreTutorial : MonoBehaviour
{
    public GameObject disattiva;
    //public GameObject controller;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    Musica m = controller.GetComponent<Musica>();
    //    m.PausaNostro();

    //}
    void Start()
    {
        disattiva.SetActive(true);
    }
}
