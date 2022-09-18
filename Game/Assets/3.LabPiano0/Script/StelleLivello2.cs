using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StelleLivello2 : MonoBehaviour
{
    public GameObject stella1;
    public GameObject stella2;
    public GameObject stella3;
    public GameObject percentuale;
    // Start is called before the first frame update
    void Start()
    {
        Text t = percentuale.GetComponent<Text>();
        t.text = NewBehaviourScript.percentualeLivello2.ToString() + "%";

        if (NewBehaviourScript.numeroStelleLivello2 == 0)
        {
            stella1.SetActive(false);
            stella2.SetActive(false);
            stella3.SetActive(false);
        }
        if (NewBehaviourScript.numeroStelleLivello2 == 1)
        {
            stella1.SetActive(true);
            stella2.SetActive(false);
            stella3.SetActive(false);
        }
        if (NewBehaviourScript.numeroStelleLivello2 == 2)
        {
            stella1.SetActive(true);
            stella2.SetActive(true);
            stella3.SetActive(false);
        }
        if (NewBehaviourScript.numeroStelleLivello2 == 3)
        {
            stella1.SetActive(true);
            stella2.SetActive(true);
            stella3.SetActive(true);
        }
    }
}
