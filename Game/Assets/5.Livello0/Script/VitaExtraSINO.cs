using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaExtraSINO : MonoBehaviour
{
    public GameObject me;
    public int livello;
    // Start is called before the first frame update
    void Start()
    {
        if (livello == 1)
        {
            if(!NewBehaviourScript.vitaExtra1)
            {
                me.SetActive(false);
            }
        }
        if (livello == 2)
        {
            if (!NewBehaviourScript.vitaExtra2)
            {
                me.SetActive(false);

            }

        }
        if (livello == 3)
        {
            if (!NewBehaviourScript.vitaExtra3)
            {
                me.SetActive(false);

            }

        }
    }
}
