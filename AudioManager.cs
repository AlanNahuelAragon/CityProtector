using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, sfxMixer;
    public AudioSource bgM, lshoot, hshoot, pshoot,lexplosion,fexplosion,hit,charging;
    public Slider musicSlider, sfxSlider;

    [Range(-80,10)]
    public float musicVol, sfxVol;
    
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
        float mV  = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float fxV = PlayerPrefs.GetFloat("SfxVolume", 0f);

        musicMixer.SetFloat("exposedMusicVol",mV);
        sfxMixer.SetFloat("exposedSfxVol", fxV);

        //Setting Slider-el orden importa
        musicSlider.minValue = -25;
        musicSlider.maxValue = 10;
        sfxSlider.minValue = -25;
        sfxSlider.maxValue = 10;
        musicSlider.value = mV;
        sfxSlider.value =fxV;
        Debug.Log("Obtengo Datos M:"+ mV+"fx:"+ fxV);

        

        

        PlayAudio(bgM);
    }

    public void MusicVolume()
    {
        musicMixer.SetFloat("exposedMusicVol", musicSlider.value);
        //Recordar asignar el Evento OnValueChanged en el slider
    }
    public void SfxVolume()
    {
        sfxMixer.SetFloat("exposedSfxVol", sfxSlider.value);
    }
    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
    public void PlayAudioPitch(AudioSource audio)
    {
        float randomPitch = Random.Range(1f, 2f);
        audio.pitch = randomPitch;
        audio.Play();
    }
}
