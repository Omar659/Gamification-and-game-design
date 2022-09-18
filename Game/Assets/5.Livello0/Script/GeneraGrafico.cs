using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneraGrafico : MonoBehaviour
{
    public GameObject asticella;
    public GameObject numero;
    public GameObject figura;
    public int livello;
    private bool f = true;
    private GameObject[] asticelle;
    private GameObject[] numeri;
    public GameObject obiettivo;
    public GameObject tutorial;
    private float mas;
    [HideInInspector]
    public float[] valori;
    [HideInInspector]
    public float[] percentuali;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void boh()
    {
        if (f)
        {
            GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
            mas = go.massimo;
        }
        Figura s = figura.GetComponent<Figura>();
        valori = new float[s.eigenvalue.Length - 2]; //autovalori
        for (int i = 1; i < s.eigenvalue.Length - 1; i++)
        {
            valori[i - 1] = (float)System.Math.Round(s.eigenvalue[i] / s.eigenvalue[s.eigenvalue.Length - 1], 2);
            valori[i - 1] = valori[i - 1] * 100;
        }
        float n = (float)valori.Length;  //numero di autovalori
        float scale = 1f / (n * 2f);
        if (f)
        {
            asticelle = new GameObject[(int)n];
            numeri = new GameObject[(int)n];
            for (int i = 0; i < n; i++)
            {
                asticelle[i] = Instantiate(asticella, transform.position, Quaternion.identity) as GameObject;
                asticelle[i].transform.SetParent(this.transform);
                asticelle[i].transform.localPosition = new Vector3(0, -0.5f, 0);
                asticelle[i].transform.localScale = new Vector3(scale, 0.5f, 0);
                numeri[i] = Instantiate(numero, transform.position, Quaternion.identity) as GameObject;
                numeri[i].transform.SetParent(this.transform);
                numeri[i].transform.localPosition = new Vector3(0, -0.5f, 0);
                numeri[i].transform.localScale = new Vector3(0.0002f, 0.0005f, 0);
                //GameObject testo = asticelle[i].transform.GetChild(0).gameObject;
                f = false;
            }
        }
        for (int i = 0; i < n; i++)
        {
            Text t = numeri[i].GetComponent<Text>();
            //float tmp
            t.text = valori[(int)n - 1 - i].ToString();
        }
        bool flag = true;
        float pos = 0.5f;
        int cont = 0;
        for (int i = 0; i < n * 2; i++)
        {
            pos -= scale;
            if (flag)
            {
                asticelle[cont].transform.localPosition = new Vector3(pos, -0.5f, 0);
                numeri[cont].transform.localPosition = new Vector3(pos, -0.875f, 0);
                flag = false;
                cont++;
            }
            else
            {
                flag = true;
            }
        }
        percentuali = new float[(int)n];
        float[] altezze = new float[(int)n];
        float distB = 0.035f;
        float dim = 0.6f;
        for (int i = 0; i < n; i++)
        {
            percentuali[i] = (valori[i] * dim) / mas;
            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(livello == 1)
        {
            if (NewBehaviourScript.tutorial1)
            {
                if (f)
                {
                    boh();
                }
                else if (!tutorial.activeSelf)
                {
                    boh();
                }
            }
            else
            {
                boh();
            }
        }
        else if (livello == 2)
        {
            if (NewBehaviourScript.tutorial2)
            {
                if (f)
                {
                    boh();
                }
                else if (!tutorial.activeSelf)
                {
                    boh();
                }
            }
            else
            {
                boh();
            }
        }
        else if (livello == 3)
        {
            if (NewBehaviourScript.tutorial3)
            {
                if (f)
                {
                    boh();
                }
                else if (!tutorial.activeSelf)
                {
                    boh();
                }
            }
            else
            {
                boh();
            }
        }

        //else if (NewBehaviourScript.tutorial2)
        //{
        //    if (f)
        //    {
        //        if (f)
        //        {
        //            GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
        //            mas = go.massimo;
        //        }
        //        Figura s = figura.GetComponent<Figura>();
        //        valori = new float[s.eigenvalue.Length - 2]; //autovalori
        //        for (int i = 1; i < s.eigenvalue.Length - 1; i++)
        //        {
        //            valori[i - 1] = (float)System.Math.Round(s.eigenvalue[i] / s.eigenvalue[s.eigenvalue.Length - 1], 2);
        //            valori[i - 1] = valori[i - 1] * 100;
        //        }
        //        float n = (float)valori.Length;  //numero di autovalori
        //        float scale = 1f / (n * 2f);
        //        if (f)
        //        {
        //            asticelle = new GameObject[(int)n];
        //            numeri = new GameObject[(int)n];
        //            for (int i = 0; i < n; i++)
        //            {
        //                asticelle[i] = Instantiate(asticella, transform.position, Quaternion.identity) as GameObject;
        //                asticelle[i].transform.SetParent(this.transform);
        //                asticelle[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                asticelle[i].transform.localScale = new Vector3(scale, 0.5f, 0);
        //                numeri[i] = Instantiate(numero, transform.position, Quaternion.identity) as GameObject;
        //                numeri[i].transform.SetParent(this.transform);
        //                numeri[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                numeri[i].transform.localScale = new Vector3(0.0002f, 0.0005f, 0);
        //                //GameObject testo = asticelle[i].transform.GetChild(0).gameObject;
        //                f = false;
        //            }
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            Text t = numeri[i].GetComponent<Text>();
        //            //float tmp
        //            t.text = valori[(int)n - 1 - i].ToString();
        //        }
        //        bool flag = true;
        //        float pos = 0.5f;
        //        int cont = 0;
        //        for (int i = 0; i < n * 2; i++)
        //        {
        //            pos -= scale;
        //            if (flag)
        //            {
        //                asticelle[cont].transform.localPosition = new Vector3(pos, -0.5f, 0);
        //                numeri[cont].transform.localPosition = new Vector3(pos, -0.875f, 0);
        //                flag = false;
        //                cont++;
        //            }
        //            else
        //            {
        //                flag = true;
        //            }
        //        }
        //        percentuali = new float[(int)n];
        //        float[] altezze = new float[(int)n];
        //        float distB = 0.035f;
        //        float dim = 0.6f;
        //        for (int i = 0; i < n; i++)
        //        {
        //            percentuali[i] = (valori[i] * dim) / mas;
        //            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
        //            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
        //            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        //        }
        //    }
        //    else if (!tutorial.activeSelf)
        //    {
        //        if (f)
        //        {
        //            GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
        //            mas = go.massimo;
        //        }
        //        Figura s = figura.GetComponent<Figura>();
        //        valori = new float[s.eigenvalue.Length - 2]; //autovalori
        //        for (int i = 1; i < s.eigenvalue.Length - 1; i++)
        //        {
        //            valori[i - 1] = (float)System.Math.Round(s.eigenvalue[i] / s.eigenvalue[s.eigenvalue.Length - 1], 2);
        //            valori[i - 1] = valori[i - 1] * 100;
        //        }
        //        float n = (float)valori.Length;  //numero di autovalori
        //        float scale = 1f / (n * 2f);
        //        if (f)
        //        {
        //            asticelle = new GameObject[(int)n];
        //            numeri = new GameObject[(int)n];
        //            for (int i = 0; i < n; i++)
        //            {
        //                asticelle[i] = Instantiate(asticella, transform.position, Quaternion.identity) as GameObject;
        //                asticelle[i].transform.SetParent(this.transform);
        //                asticelle[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                asticelle[i].transform.localScale = new Vector3(scale, 0.5f, 0);
        //                numeri[i] = Instantiate(numero, transform.position, Quaternion.identity) as GameObject;
        //                numeri[i].transform.SetParent(this.transform);
        //                numeri[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                numeri[i].transform.localScale = new Vector3(0.0002f, 0.0005f, 0);
        //                //GameObject testo = asticelle[i].transform.GetChild(0).gameObject;
        //                f = false;
        //            }
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            Text t = numeri[i].GetComponent<Text>();
        //            //float tmp
        //            t.text = valori[(int)n - 1 - i].ToString();
        //        }
        //        bool flag = true;
        //        float pos = 0.5f;
        //        int cont = 0;
        //        for (int i = 0; i < n * 2; i++)
        //        {
        //            pos -= scale;
        //            if (flag)
        //            {
        //                asticelle[cont].transform.localPosition = new Vector3(pos, -0.5f, 0);
        //                numeri[cont].transform.localPosition = new Vector3(pos, -0.875f, 0);
        //                flag = false;
        //                cont++;
        //            }
        //            else
        //            {
        //                flag = true;
        //            }
        //        }
        //        percentuali = new float[(int)n];
        //        float[] altezze = new float[(int)n];
        //        float distB = 0.035f;
        //        float dim = 0.6f;
        //        for (int i = 0; i < n; i++)
        //        {
        //            percentuali[i] = (valori[i] * dim) / mas;
        //            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
        //            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
        //            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        //        }

        //    }

        //}
        //else if (NewBehaviourScript.tutorial3)
        //{
        //    if (f)
        //    {
        //        if (f)
        //        {
        //            GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
        //            mas = go.massimo;
        //        }
        //        Figura s = figura.GetComponent<Figura>();
        //        valori = new float[s.eigenvalue.Length - 2]; //autovalori
        //        for (int i = 1; i < s.eigenvalue.Length - 1; i++)
        //        {
        //            valori[i - 1] = (float)System.Math.Round(s.eigenvalue[i] / s.eigenvalue[s.eigenvalue.Length - 1], 2);
        //            valori[i - 1] = valori[i - 1] * 100;
        //        }
        //        float n = (float)valori.Length;  //numero di autovalori
        //        float scale = 1f / (n * 2f);
        //        if (f)
        //        {
        //            asticelle = new GameObject[(int)n];
        //            numeri = new GameObject[(int)n];
        //            for (int i = 0; i < n; i++)
        //            {
        //                asticelle[i] = Instantiate(asticella, transform.position, Quaternion.identity) as GameObject;
        //                asticelle[i].transform.SetParent(this.transform);
        //                asticelle[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                asticelle[i].transform.localScale = new Vector3(scale, 0.5f, 0);
        //                numeri[i] = Instantiate(numero, transform.position, Quaternion.identity) as GameObject;
        //                numeri[i].transform.SetParent(this.transform);
        //                numeri[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                numeri[i].transform.localScale = new Vector3(0.0002f, 0.0005f, 0);
        //                //GameObject testo = asticelle[i].transform.GetChild(0).gameObject;
        //                f = false;
        //            }
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            Text t = numeri[i].GetComponent<Text>();
        //            //float tmp
        //            t.text = valori[(int)n - 1 - i].ToString();
        //        }
        //        bool flag = true;
        //        float pos = 0.5f;
        //        int cont = 0;
        //        for (int i = 0; i < n * 2; i++)
        //        {
        //            pos -= scale;
        //            if (flag)
        //            {
        //                asticelle[cont].transform.localPosition = new Vector3(pos, -0.5f, 0);
        //                numeri[cont].transform.localPosition = new Vector3(pos, -0.875f, 0);
        //                flag = false;
        //                cont++;
        //            }
        //            else
        //            {
        //                flag = true;
        //            }
        //        }
        //        percentuali = new float[(int)n];
        //        float[] altezze = new float[(int)n];
        //        float distB = 0.035f;
        //        float dim = 0.6f;
        //        for (int i = 0; i < n; i++)
        //        {
        //            percentuali[i] = (valori[i] * dim) / mas;
        //            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
        //            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
        //            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        //        }
        //    }
        //    else if (!tutorial.activeSelf)
        //    {
        //        if (f)
        //        {
        //            GraficoObiettivo go = obiettivo.GetComponent<GraficoObiettivo>();
        //            mas = go.massimo;
        //        }
        //        Figura s = figura.GetComponent<Figura>();
        //        valori = new float[s.eigenvalue.Length - 2]; //autovalori
        //        for (int i = 1; i < s.eigenvalue.Length - 1; i++)
        //        {
        //            valori[i - 1] = (float)System.Math.Round(s.eigenvalue[i] / s.eigenvalue[s.eigenvalue.Length - 1], 2);
        //            valori[i - 1] = valori[i - 1] * 100;
        //        }
        //        float n = (float)valori.Length;  //numero di autovalori
        //        float scale = 1f / (n * 2f);
        //        if (f)
        //        {
        //            asticelle = new GameObject[(int)n];
        //            numeri = new GameObject[(int)n];
        //            for (int i = 0; i < n; i++)
        //            {
        //                asticelle[i] = Instantiate(asticella, transform.position, Quaternion.identity) as GameObject;
        //                asticelle[i].transform.SetParent(this.transform);
        //                asticelle[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                asticelle[i].transform.localScale = new Vector3(scale, 0.5f, 0);
        //                numeri[i] = Instantiate(numero, transform.position, Quaternion.identity) as GameObject;
        //                numeri[i].transform.SetParent(this.transform);
        //                numeri[i].transform.localPosition = new Vector3(0, -0.5f, 0);
        //                numeri[i].transform.localScale = new Vector3(0.0002f, 0.0005f, 0);
        //                //GameObject testo = asticelle[i].transform.GetChild(0).gameObject;
        //                f = false;
        //            }
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            Text t = numeri[i].GetComponent<Text>();
        //            //float tmp
        //            t.text = valori[(int)n - 1 - i].ToString();
        //        }
        //        bool flag = true;
        //        float pos = 0.5f;
        //        int cont = 0;
        //        for (int i = 0; i < n * 2; i++)
        //        {
        //            pos -= scale;
        //            if (flag)
        //            {
        //                asticelle[cont].transform.localPosition = new Vector3(pos, -0.5f, 0);
        //                numeri[cont].transform.localPosition = new Vector3(pos, -0.875f, 0);
        //                flag = false;
        //                cont++;
        //            }
        //            else
        //            {
        //                flag = true;
        //            }
        //        }
        //        percentuali = new float[(int)n];
        //        float[] altezze = new float[(int)n];
        //        float distB = 0.035f;
        //        float dim = 0.6f;
        //        for (int i = 0; i < n; i++)
        //        {
        //            percentuali[i] = (valori[i] * dim) / mas;
        //            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
        //            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
        //            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        //        }

        //    }

        //}


    }
}
