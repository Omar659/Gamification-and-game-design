using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caricamento : MonoBehaviour
{
    public string livello;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        for (int i = 11; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        for (int i = 4; i < 11; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        for (int i = 0; i < 4; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(livello);
    }

}
