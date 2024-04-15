using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void MusicData(float value)
    {
        Debug.Log("Guardando Musica: "+value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
    public void SfxData(float value)
    {
        Debug.Log("Guardando SFX: "+ value);
        PlayerPrefs.SetFloat("SfxVolume", value);
        PlayerPrefs.Save();
    }
    public void HiScoreData(string min,string sec)
    {
        Debug.Log("Guardando "+min+":"+sec);
        PlayerPrefs.SetString("HiScoreMin", min);
        PlayerPrefs.SetString("HiScoreSec", sec);
        PlayerPrefs.Save();
    }
}
