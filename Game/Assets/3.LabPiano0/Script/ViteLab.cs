using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViteLab : MonoBehaviour
{
    public GameObject testo;
    // Start is called before the first frame update
    void Start()
    {
        Text t = testo.GetComponent<Text>();
        t.text = "x" + NewBehaviourScript.vite.ToString();
    }
}
