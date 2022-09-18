using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StelleLab : MonoBehaviour
{
    public GameObject testo;
    // Start is called before the first frame update
    void Start()
    {
        Text t = testo.GetComponent<Text>();
        t.text = "x" + (NewBehaviourScript.numeroStelleLivello1 + NewBehaviourScript.numeroStelleLivello2 + NewBehaviourScript.numeroStelleLivello3).ToString();
    }
}
