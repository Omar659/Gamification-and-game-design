using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject caricamento;
    public GameObject i1;
    public GameObject i2;
    public GameObject i3;
    public int i;
    public GameObject continua;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int cont = 0;
        int cont1 = 0;
        while (cont1<i)
        {
            if (cont == 0)
            {
                yield return new WaitForSeconds(0.5f);
                cont++;
                i1.SetActive(true);
                i2.SetActive(false);
                i3.SetActive(false);
            }
            else if (cont == 1)
            {
                yield return new WaitForSeconds(0.5f);
                cont++;
                i1.SetActive(true);
                i2.SetActive(true);
                i3.SetActive(false);
            }
            else if (cont == 2)
            {
                yield return new WaitForSeconds(0.5f);
                cont++;
                i1.SetActive(true);
                i2.SetActive(true);
                i3.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                i1.SetActive(false);
                i2.SetActive(false);
                i3.SetActive(false);
                cont = 0;
            }
            cont1++;
        }
        continua.SetActive(true);
        caricamento.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
