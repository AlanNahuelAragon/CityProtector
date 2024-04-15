using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    void Awake()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isPaused = true;
            
        }
    }
    public void ContinueGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            isPaused = false;
            DataManager.instance.MusicData(AudioManager.instance.musicSlider.value);
            DataManager.instance.SfxData(AudioManager.instance.sfxSlider.value);
            
        }
    }

}
