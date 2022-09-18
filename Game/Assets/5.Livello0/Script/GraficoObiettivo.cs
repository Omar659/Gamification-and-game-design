using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;

public class GraficoObiettivo : MonoBehaviour
{
    public TextAsset autovalori;
    public GameObject asticella;
    public GameObject numero;
    private GameObject[] asticelle;
    private GameObject[] numeri;
    [HideInInspector]
    public float massimo;
    [HideInInspector]
    public float[] valori;
    // Start is called before the first frame update
    void Start()
    {
        string offString = autovalori.ToString();
        string[] lines = offString.Split(new[] { "\n" }, StringSplitOptions.None);
        NumberFormatInfo provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";
        float[] valori1 = new float[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            valori1[i] = (float)System.Math.Round(((float)Convert.ToDouble(lines[i], provider)), 2);
        }
        valori = new float[lines.Length-1];
        for (int i = 0; i < valori.Length; i++)
        {
            valori[i] = (float)System.Math.Round(valori1[i] / valori1[valori1.Length - 1], 2);
            valori[i] = valori[i] * 100;
        }
        massimo = valori[valori.Length-1];

        float n = (float)valori.Length;  //numero di autovalori
        float scale = 1f / (n * 2f);
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
        float[] percentuali = new float[(int)n];
        float[] altezze = new float[(int)n];
        float distB = 0.035f;
        float dim = 0.6f;
        for (int i = 0; i < n; i++)
        {
            percentuali[i] = (valori[i] * dim) / valori[(int)n - 1];
            altezze[i] = ((dim - percentuali[i]) / 2f) - distB;
            asticelle[(int)n - 1 - i].transform.localScale = new Vector3(scale, percentuali[i], 0f);
            asticelle[(int)n - 1 - i].transform.localPosition += Vector3.down * altezze[i];
        }
    }
}
