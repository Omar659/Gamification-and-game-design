using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BarraPercentuale : MonoBehaviour
{
    public GameObject obiettivo;
    public GameObject nostro;
    public GameObject percentuale;
    public GameObject tasto;
    private float[] autovaloriObiettivo;
    private float[] autovaloriNostri;
    private float mas;
    private float xInizio;
    public float percentualeMinima;
    public float dueStelle;
    public float treStelle;
    [HideInInspector]
    public float ris;
    [HideInInspector]
    public float[] percentuali;

    // Start is called before the first frame update
    void Start()
    {
        mas = this.transform.localScale.x;
        xInizio = this.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
        GeneraGrafico gn = nostro.GetComponent<GeneraGrafico>();
        autovaloriNostri = gn.valori;
        autovaloriObiettivo = go.valori;
        percentuali = new float[autovaloriObiettivo.Length];

        //parametri da testare in gioco che indicano il massimo e il minimo per ottenere 0% da un autovalore
        float min = 1f;
        float max = 1f;
        //ovvero per minimo si intende quando il nostro autovalore diventa piu picccolo dell'obiettivo

        float percSomma = 0f;
        for (int i = 0; i < autovaloriObiettivo.Length; i++)
        {
            float dist = Math.Abs(autovaloriNostri[i] - autovaloriObiettivo[i]);

            if (autovaloriNostri[i] < autovaloriObiettivo[i])
            {
                percentuali[i] = (autovaloriNostri[i] * 100) / autovaloriObiettivo[i];
            //    //100 * dist / max
            //    float dist = Math.Abs(autovaloriNostri[i] - autovaloriObiettivo[i]);
            //    print(dist);
            //    percentuali[i] = 100 - (100f * dist) / (max * autovaloriObiettivo[i]);
            }
            else
            {
                percentuali[i] = (autovaloriObiettivo[i] * 100) / autovaloriNostri[i];
            //    float dist = Math.Abs(autovaloriNostri[i] - autovaloriObiettivo[i]);
            //    percentuali[i] = 100 - (100f * dist) / (min * autovaloriObiettivo[i]);
            }
            ////print(i+": "+percentuali[i]);
            percSomma += percentuali[i];
        }


        ris = percSomma / autovaloriObiettivo.Length;
        Text testo = percentuale.GetComponent<Text>();
        testo.text = ((float)System.Math.Round(ris, 0)).ToString() + "%";



        float p = (ris * mas) / 100;
        this.transform.localScale = new Vector3(p, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.localPosition = new Vector3(xInizio - (mas - p) / 2f, this.transform.localPosition.y, this.transform.localPosition.z);
        if (ris > percentualeMinima)
        {
            tasto.SetActive(true);
        }
        else
        {
            tasto.SetActive(false);
        }
    }
}
