using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvviaStelle : MonoBehaviour
{
    public GameObject stelle;
    public GameObject menuVittoria;
    private bool attivo = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuVittoria.activeSelf && attivo)
        {
            attivo = false;
            stelle.SetActive(true);
        }
    }
}
