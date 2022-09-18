using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLab : MonoBehaviour
{
    public GameObject inizio;
    public GameObject livello1;
    public GameObject livello3;
    public GameObject gravità;
    // Start is called before the first frame update
    void Start()
    {
        if(NewBehaviourScript.tutorialLab == 1)
        {
            inizio.SetActive(true);
        }
        else if (NewBehaviourScript.tutorialLab == 2)
        {
            livello1.SetActive(true);
        }
        else if (NewBehaviourScript.tutorialLab == 3)
        {
            livello3.SetActive(true);
            gravità.SetActive(true);
        }
        else if (NewBehaviourScript.tutorialLab == 4)
        {
            gravità.SetActive(false);
            inizio.SetActive(false);
            livello1.SetActive(false);
            livello3.SetActive(false);
        }
        else
        {
            gravità.SetActive(true);
            inizio.SetActive(false);
            livello1.SetActive(false);
            livello3.SetActive(false);
        }
    }
}
