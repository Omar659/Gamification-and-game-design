using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tempo : MonoBehaviour
{
    public float mainTimer;
    public GameObject menuVita;
    public GameObject imVite;
    public GameObject menuSconfitta;
    public GameObject menu;
    public GameObject vittoria;
    public GameObject percentule;
    public GameObject cervello;
    public GameObject tutorial;



    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool vita = true;

    private bool canCount = true;
    private bool doOnce = false;
    private float mas;
    private float yInizio;

    // Start is called before the first frame update
    void Start()
    {
        mas = this.transform.localScale.y;
        yInizio = this.transform.localPosition.y;
        timer = mainTimer;

        AttivaVite();
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu.activeSelf && !vittoria.activeSelf && !tutorial.activeSelf)
        {
            if (timer >= 0.0f && canCount)
            {
                //mainTimer: timer = dimmax:x
                float p = (timer * mas) / mainTimer;
                timer -= Time.deltaTime;
                if(timer < ((25*mainTimer)/100) && vita)
                {
                    vita = false;
                    cervello.SetActive(false);
                }
                this.transform.localScale = new Vector3(this.transform.localScale.x, p, this.transform.localScale.z);
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, yInizio - (mas - p) / 2f, this.transform.localPosition.z);
            }
            else if (timer <= 0.0f && !doOnce)
            {

                canCount = false;
                doOnce = true;
                // = 0.00f
                timer = 0.0f;

                if (NewBehaviourScript.vite > 0)
                {
                    menuVita.SetActive(true);
                }
                else
                {
                    menuSconfitta.SetActive(true);
                }
            }
        }
    }

    public void ResetBtn()
    {
        if (timer <= 0.0f && doOnce && !canCount)
        {
            NewBehaviourScript.vite--;
            AttivaVite();
            this.transform.localScale = new Vector3(this.transform.localScale.x, mas, this.transform.localScale.z);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, yInizio, this.transform.localPosition.z);
            timer = mainTimer;
            canCount = true;
            doOnce = false;
        }
    }

    public void AttivaVite()
    {
        Text t = imVite.GetComponent<Text>();
        t.text = "x" + NewBehaviourScript.vite.ToString();
        //for (int i = 0; i < imVite.transform.childCount; i++)
        //{
        //    //if (i<NewBehaviourScript.vite)
        //    //{
        //    //    imVite.transform.GetChild(i).gameObject.SetActive(true);
        //    //}
        //    //else
        //    //{
        //    //    imVite.transform.GetChild(i).gameObject.SetActive(false);
        //    //}
        //}
    }
}
