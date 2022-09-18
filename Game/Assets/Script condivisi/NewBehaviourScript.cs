using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //livello1
    public static float percentualeLivello1 = 0f;
    public static float numeroStelleLivello1 = 0f;
    public static bool vitaExtra1 = true;
    public static bool tutorial1 = true;
    //public static bool tutorial1 = true;

    //livello2
    public static float percentualeLivello2 = 0f;
    public static float numeroStelleLivello2 = 0f;
    public static bool vitaExtra2 = true;
    public static bool tutorial2 = true;

    //livello3
    public static float percentualeLivello3 = 0f;
    public static float numeroStelleLivello3 = 0f;
    public static bool vitaExtra3 = true;
    public static bool tutorial3 = true;

    //elementi di gioco
    public static int vite = 3;
    public static bool chiave3d = true;
    public static bool chiave2d = false;
    public static bool consumabile = true;
    public static bool FINE = false;
    public static int tutorialLab = 1;

    public GameObject lutrario;
    public GameObject menuVittoria;
    public GameObject menuSconfitta;
    public GameObject percentule;
    public GameObject chiaveInventario;
    public GameObject daUsare;

    private void Start()
    {

    }
    public void Play()
    {
        SceneManager.LoadScene("3.LabPiano0");
    }

    public void Livello1()
    {
        Camera.posizione = lutrario.transform.position;
        tutorial1 = true;
        SceneManager.LoadScene("4.Caricamento");
    }
    public void Livello2()
    {
        Camera.posizione = lutrario.transform.position;
        tutorial2 = true;
        SceneManager.LoadScene("4.Caricamento1");
    }
    public void Livello3()
    {
        Camera.posizione = lutrario.transform.position;
        tutorial3 = true;
        chiave2d = false;
        consumabile = false;
        SceneManager.LoadScene("4.Caricamento2");
    }

    //public void ContinuaLivello1()
    //{
    //    SceneManager.LoadScene("5.Livello0");
    //}

    public void RicominciaLivello1()
    {
        tutorial1 = false;
        SceneManager.LoadScene("5.Livello0");
    }

    public void RicominciaLivello2()
    {
        tutorial2 = false;
        SceneManager.LoadScene("5.Livello1");
    }

    public void RicominciaLivello3()
    {
        tutorial3 = false;
        SceneManager.LoadScene("5.Livello2");
    }

    public void TornaAlLaboratorio1()
    {
        tutorial1 = true;
        Camera.posizione = new Vector3(-22.99604f, 0.7908f, -163.002f);
        SceneManager.LoadScene("3.LabPiano0");
    }
    public void TornaAlLaboratorio2()
    {
        tutorial2 = true;
        Camera.posizione = new Vector3(23.90005f, 0.7908f, -151.7979f);
        SceneManager.LoadScene("3.LabPiano0");
    }
    public void TornaAlLaboratorio3()
    {
        tutorialLab = 3;
        tutorial3 = true;
        Camera.posizione = new Vector3(-47.34048f, 0.7908f, -138.9474f);
        SceneManager.LoadScene("3.LabPiano0");
    }

    public void TutorialAvantiLab1()
    {
        tutorialLab = 2;
    }
    public void TutorialAvantiLab2()
    {
        tutorialLab = 4;
    }
    public void TutorialAvantiLab3()
    {
        tutorialLab = 0;
    }

    public void MenuViteNo()
    {
        BarraPercentuale b = percentule.GetComponent<BarraPercentuale>();
        float perc = b.ris;
        float min = b.percentualeMinima;
        if (perc > min)
        {
            menuVittoria.SetActive(true);
        }
        else
        {
            menuSconfitta.SetActive(true);
        }
    }
}
