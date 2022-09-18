using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitoliDiCoda : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        NewBehaviourScript.percentualeLivello1 = 0f;
        NewBehaviourScript.numeroStelleLivello1 = 0f;
        NewBehaviourScript.vitaExtra1 = true;
        NewBehaviourScript.tutorial1 = true;
        //public static bool tutorial1 = true;

        //livello2
        NewBehaviourScript.percentualeLivello2 = 0f;
        NewBehaviourScript.numeroStelleLivello2 = 0f;
        NewBehaviourScript.vitaExtra2 = true;
        NewBehaviourScript.tutorial2 = true;

        //livello3
        NewBehaviourScript.percentualeLivello3 = 0f;
        NewBehaviourScript.numeroStelleLivello3 = 0f;
        NewBehaviourScript.vitaExtra3 = true;
        NewBehaviourScript.tutorial3 = true;

        //elementi di gioco
        NewBehaviourScript.vite = 3;
        NewBehaviourScript.chiave3d = true;
        NewBehaviourScript.chiave2d = false;
        NewBehaviourScript.consumabile = true;
        NewBehaviourScript.FINE = false;
        NewBehaviourScript.tutorialLab = 1;

        Camera.posizione = new Vector3(0f, 0.489f, -186f);
        yield return new WaitForSeconds(53f);
        SceneManager.LoadScene("2.Inizio");


    }

    public void Fine()
    {
        SceneManager.LoadScene("2.Inizio");
    }
}
