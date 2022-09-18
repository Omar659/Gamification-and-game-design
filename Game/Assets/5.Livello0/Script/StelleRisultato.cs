using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System;

public class StelleRisultato : MonoBehaviour
{
    public GameObject MenuVittoria;
    public GameObject percentuale;
    public GameObject percentualeText;
    public GameObject stelle;
    public GameObject vitaExtra;
    public GameObject tempo;
    public int livello;
    public bool hoFinitoLaFantasia = false;

    private bool vittoria = true;
    private float timer;
    IEnumerator Start()
    {
        timer = 0.6f;
        vittoria = false;
        BarraPercentuale b = percentuale.GetComponent<BarraPercentuale>();
        int stelle;
        float uno = b.percentualeMinima;
        float due = b.dueStelle;
        float tre = b.treStelle;
        float percOttenuta = (float)Math.Round(b.ris, 0);
        Text t = percentualeText.GetComponent<Text>();
        t.text = percOttenuta.ToString() + "%";
        print(percOttenuta);


        Tempo tem = tempo.GetComponent<Tempo>();
        if (livello == 1)
        {
            if ((tem.timer * 100) / tem.mainTimer > 25f && tem.vita && NewBehaviourScript.vitaExtra1)
            {
                NewBehaviourScript.vitaExtra1 = false;
                NewBehaviourScript.vite++;
                hoFinitoLaFantasia = true;
                //vitaExtra.SetActive(true);
            }
        }
        else if (livello == 2)
        {
            if ((tem.timer * 100) / tem.mainTimer > 25f && tem.vita && NewBehaviourScript.vitaExtra2)
            {
                NewBehaviourScript.vitaExtra2 = false;
                NewBehaviourScript.vite++;
                hoFinitoLaFantasia = true;
                //vitaExtra.SetActive(true);
            }
        }
        else if (livello == 3)
        {
            if ((tem.timer * 100) / tem.mainTimer > 25f && tem.vita && NewBehaviourScript.vitaExtra3)
            {
                NewBehaviourScript.vitaExtra3 = false;
                NewBehaviourScript.vite++;
                hoFinitoLaFantasia = true;
                //vitaExtra.SetActive(true);
            }
        }


        if (percOttenuta >= uno && percOttenuta < due)
        {
            stelle = 1;
        }
        else if (percOttenuta >= due && percOttenuta < tre)
        {
            stelle = 2;
        }
        else if (percOttenuta >= tre)
        {
            stelle = 3;
        }
        else
        {
            stelle = 0;
        }
        if (livello == 1)
        {
            if (stelle > NewBehaviourScript.numeroStelleLivello1)
            {
                NewBehaviourScript.numeroStelleLivello1 = stelle;
            }
            if (percOttenuta > NewBehaviourScript.percentualeLivello1)
            {
                NewBehaviourScript.percentualeLivello1 = percOttenuta;
            }
        }
        else if (livello == 2)
        {
            if (stelle > NewBehaviourScript.numeroStelleLivello2)
            {
                NewBehaviourScript.numeroStelleLivello2 = stelle;
            }
            if (percOttenuta > NewBehaviourScript.percentualeLivello2)
            {
                NewBehaviourScript.percentualeLivello2 = percOttenuta;
            }
        }
        else if (livello == 3)
        {
            if (stelle > NewBehaviourScript.numeroStelleLivello3)
            {
                NewBehaviourScript.numeroStelleLivello3 = stelle;
            }
            if (percOttenuta > NewBehaviourScript.percentualeLivello3)
            {
                NewBehaviourScript.percentualeLivello3 = percOttenuta;
            }
        }

        if (percOttenuta >= uno && percOttenuta < due)
        {
            yield return new WaitForSeconds(timer);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (percOttenuta >= due && percOttenuta < tre)
        {
            yield return new WaitForSeconds(timer);
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(timer);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (percOttenuta >= tre)
        {
            yield return new WaitForSeconds(timer);
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(timer);
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(timer);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(timer*1.5f);
        percentualeText.SetActive(true);
        yield return new WaitForSeconds(timer);

        if (hoFinitoLaFantasia)
        {
            vitaExtra.SetActive(true);
        }
        

       
    }
}
