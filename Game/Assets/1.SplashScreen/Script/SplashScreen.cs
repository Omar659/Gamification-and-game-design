using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen: MonoBehaviour
{
    public Image sfondo;
    public Image titolo;
    public Image sfondoGam;
    public Image gamification;
    public Image scritta;
    public string loadLevel;

    IEnumerator Start()
    {
        sfondo.canvasRenderer.SetAlpha(0.0f);
        titolo.canvasRenderer.SetAlpha(0.0f);
        sfondoGam.canvasRenderer.SetAlpha(0.0f);
        gamification.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(3.7f);
        FadeOut();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(loadLevel);
    }

    void FadeIn()
    {
        sfondo.CrossFadeAlpha(1.0f, 1.7f, false);
        titolo.CrossFadeAlpha(1.0f, 1.7f, false);
        sfondoGam.CrossFadeAlpha(1.0f, 1.7f, false);
        gamification.CrossFadeAlpha(1.0f, 1.7f, false);
    }

    void FadeOut()
    {
        sfondo.CrossFadeAlpha(0.0f, 2.0f, false);
        titolo.CrossFadeAlpha(0.0f, 2.0f, false);
        sfondoGam.CrossFadeAlpha(0.0f, 2.0f, false);
        gamification.CrossFadeAlpha(0.0f, 2.0f, false);
    }
}
