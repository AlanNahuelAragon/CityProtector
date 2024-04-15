using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public bool isRunning;

    public static ScoreTimer instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartTimer();
    }
    void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
        StartCoroutine(UpdateTimer());
    }
    IEnumerator UpdateTimer()
    {
        while (isRunning)
        {
            float timePassed = Time.time - startTime;

            // Formatear el tiempo en minutos y segundos
            string minutes = Mathf.Floor(timePassed / 60).ToString("00");
            string seconds = Mathf.Floor(timePassed % 60).ToString("00");

            // Actualizar el texto del temporizador
            timerText.text = minutes + ":" + seconds;

            yield return new WaitForSeconds(1f); // Esperar un segundo antes de actualizar de nuevo
        }
    }
}
