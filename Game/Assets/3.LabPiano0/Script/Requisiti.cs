using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Requisiti : MonoBehaviour
{
    public GameObject stelle;
    public GameObject livelli;

    // Start is called before the first frame update
    void Start()
    {
        Text st = stelle.GetComponent<Text>();
        Text lv = livelli.GetComponent<Text>();
        int lvlr = 0;
        st.text = (NewBehaviourScript.numeroStelleLivello1 + NewBehaviourScript.numeroStelleLivello2 + NewBehaviourScript.numeroStelleLivello3).ToString() + "/6";
        if (NewBehaviourScript.numeroStelleLivello1 > 0) lvlr++;
        if (NewBehaviourScript.numeroStelleLivello2 > 0) lvlr++;
        if (NewBehaviourScript.numeroStelleLivello3 > 0) lvlr++;
        lv.text = lvlr.ToString() + "/3";

    }
}
