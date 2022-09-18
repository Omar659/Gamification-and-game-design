using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAttivo : MonoBehaviour
{
    public GameObject tutorial;
    public int livello;
    // Start is called before the first frame update
    void Awake()
    {
        if (livello == 1)
        {
            if (!NewBehaviourScript.tutorial1)
            {
                tutorial.SetActive(false);
            }
        }
        else if (livello == 2)
        {
            if (!NewBehaviourScript.tutorial2)
            {
                tutorial.SetActive(false);
            }
        }
        else if (livello == 3)
        {
            if (!NewBehaviourScript.tutorial3)
            {
                tutorial.SetActive(false);
            }
        }
    }
}
