using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    public GameObject[] audio;
    public GameObject graficoNostro;
    public GameObject percentuale;
    public GameObject menuVite;
    public GameObject menuSconfitta;

    private AudioSource[] m;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange = false;
    bool daBottone = false;

    //public void PlayObiettivo()
    //{

    //}
    //public void PausaObiettivo()
    //{

    //}

    public void PausaButton()
    {
        if (m_ToggleChange)
        {
            daBottone = true;
        }
        PausaNostro();
    }


    public void PlayButton()
    {
        if (daBottone)
        {
            PlayNostro();
            daBottone = false;
        }
    }

    public void PlayNostro()
    {
        m_ToggleChange = true;
    }
    public void PausaNostro()
    {
        m_ToggleChange = false;
    }

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        //m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
        m = new AudioSource[audio.Length];
        for (int i = 0; i < audio.Length; i++)
        {
            m[i] = audio[i].GetComponent<AudioSource>();
        }
        if (!NewBehaviourScript.tutorial1)
        {
            m_ToggleChange = true;
            for (int i = 0; i < m.Length; i++)
            {
                m[i].Play();
            }
        }
        else if (!NewBehaviourScript.tutorial2)
        {
            m_ToggleChange = true;
            for (int i = 0; i < m.Length; i++)
            {
                m[i].Play();
            }
        }
        else if (!NewBehaviourScript.tutorial3)
        {
            m_ToggleChange = true;
            for (int i = 0; i < m.Length; i++)
            {
                m[i].Play();
            }
        }
    }

    void Update()
    {
        //AudioSource audio = GetComponents<AudioSource>();
        //audio.clip = audioClips[clipNumber];
        //audio.Play();
        if (menuSconfitta.activeSelf || menuVite.activeSelf)
        {
            PausaButton();
        }
        BarraPercentuale bp = percentuale.GetComponent<BarraPercentuale>();
        //GeneraGrafico gg = graficoNostro.GetComponent<GeneraGrafico>();
        for (int i = 0; i < bp.percentuali.Length; i++)
        {
            m[i].volume = ((bp.percentuali[i]/100f)*40f)/100;
        }
        m[m.Length-1].volume = ((((100f - bp.ris) / 100f) / 2f)*30f)/100;


        //Check to see if you just set the toggle to positive
        if (m_ToggleChange && m_Play)
        {
            print(bp.percentuali.Length);
            for (int i = 0; i < bp.percentuali.Length; i++)
            {
                m[i].Play();
            }
            m[m.Length - 1].Play();
            m_Play = false;
        }
        //Check if you just set the toggle to false
        if (!m_ToggleChange && !m_Play)
        {
            for (int i = 0; i < audio.Length; i++)
            {
                m[i].Pause();
            }
            m_Play = true;
        }
    }

}
