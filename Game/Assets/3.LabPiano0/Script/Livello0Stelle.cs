using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livello0Stelle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (NewBehaviourScript.numeroStelleLivello1 == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (NewBehaviourScript.numeroStelleLivello1 == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (NewBehaviourScript.numeroStelleLivello1 == 2)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);

        }else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);

        }
    }
}
