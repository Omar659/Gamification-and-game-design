using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScenaETastoBack : MonoBehaviour
{
    public GameObject salta;
    public GameObject musica;
    private float timer = 10;
    private AudioSource a;
    private bool abbassa = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        a = musica.GetComponent<AudioSource>();
        yield return new WaitForSeconds(10f);
        salta.SetActive(true);
        yield return new WaitForSeconds(70f);
        abbassa = true;
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("3.LabPiano0");
    }
    private void Update()
    {
        if (abbassa)
        {
            timer -= Time.deltaTime;
            a.volume = timer / 10f;
        }
    }
}
