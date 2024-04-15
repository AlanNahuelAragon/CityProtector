using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    IEnumerator BackToMenuCoroutine()
    {
        GOFade.instance.StartFadeIn();
        while (GOFade.instance.fadingIn)
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
    IEnumerator NewGameCoroutine()
    {
        GOFade.instance.StartFadeIn();
        while (GOFade.instance.fadingIn)
        {
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
    IEnumerator CloseAppCoroutine()
    {
        GOFade.instance.StartFadeIn();
        while (GOFade.instance.fadingIn)
        {
            yield return null;
        }
        Application.Quit();
    }
    public void BackToMenu()
    {
        DataManager.instance.MusicData(AudioManager.instance.musicSlider.value);
        DataManager.instance.SfxData(AudioManager.instance.sfxSlider.value);
        Time.timeScale = 1f;
        StartCoroutine(BackToMenuCoroutine());
    }
    public void NewGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(NewGameCoroutine());
    }
    public void CloseApp()
    {
        StartCoroutine (CloseAppCoroutine());
    }
}
