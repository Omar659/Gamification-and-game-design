using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    public GameObject cubo;
    public GameObject FINE;
    private bool click = false;
    GameObject sopra1;
    GameObject sopra2;
    GameObject sotto1;
    GameObject sotto2;
    float timein;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        sopra1 = transform.GetChild(0).gameObject;
        sopra2 = sopra1.transform.GetChild(0).gameObject;
        sotto1 = transform.GetChild(1).gameObject;
        sotto2 = sotto1.transform.GetChild(0).gameObject;
        timein = 2f;
        timer = timein;
    }
    public void Apri()
    {
        click = true;
    }

    // Update is called once per frame
    void Update()
    {
        //apro
        if (click)
        {
            if(timer>0f)
            {
                timer -= Time.deltaTime;
                sopra1.transform.localPosition = new Vector3(sopra1.transform.localPosition.x, 2 + (1-((1f * timer) / timein)), sopra1.transform.localPosition.z);
                sopra2.transform.localPosition = new Vector3(sopra2.transform.localPosition.x, 0 + (0.9f - ((0.9f * timer) / timein)), sopra2.transform.localPosition.z);
                sotto1.transform.localPosition = new Vector3(sotto1.transform.localPosition.x, 2 - (1f - ((1f * timer) / timein)), sotto1.transform.localPosition.z);
                sotto2.transform.localPosition = new Vector3(sotto2.transform.localPosition.x, 0 - (0.9f - ((0.9f * timer) / timein)), sotto2.transform.localPosition.z);
            }
            else
            {
                cubo.SetActive(false);
                click = false;
                timer = timein;
            }
        }
        if (NewBehaviourScript.FINE)
        {
            if (timer > 0f)
            {
                cubo.SetActive(true);
                timer -= Time.deltaTime;
                sopra1.transform.localPosition = new Vector3(sopra1.transform.localPosition.x, 3 - (1 - ((1f * timer) / timein)), sopra1.transform.localPosition.z);
                sopra2.transform.localPosition = new Vector3(sopra2.transform.localPosition.x, 0.9f - (0.9f - ((0.9f * timer) / timein)), sopra2.transform.localPosition.z);
                sotto1.transform.localPosition = new Vector3(sotto1.transform.localPosition.x, 1f + (1f - ((1f * timer) / timein)), sotto1.transform.localPosition.z);
                sotto2.transform.localPosition = new Vector3(sotto2.transform.localPosition.x, -0.9f + (0.9f - ((0.9f * timer) / timein)), sotto2.transform.localPosition.z);
            }
            else
            {
                FINE.SetActive(true);
                NewBehaviourScript.FINE = false;
            }
        }
    }
}
