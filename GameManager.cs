using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject defeatScreen;
    public GameObject spawnerGO;
    public GameObject scoretimerGO;
    public GameObject finalExplosionPrefab;
    public Text hiScoreText;
    public GameObject flashingNew;
    public float flashEnter, flashExit;

    public Text lastRecord;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.fexplosion);
            GameObject newFinalExp = Instantiate(finalExplosionPrefab,other.transform.position,other.transform.rotation);
            ShowDefeat();
        }
    }
    IEnumerator ShowDefeatCoroutine()
    {
        float duration = 2f;
        float startVolume = AudioManager.instance.musicSlider.value;
        float endVolume = -20f;

        EnemySpawner enemySpawner = spawnerGO.GetComponent<EnemySpawner>();
        enemySpawner.isTimeRunning = false;
        ScoreTimer scoreTimer = scoretimerGO.GetComponent<ScoreTimer>();
        scoreTimer.isRunning = false;
        GOFade.instance.StartFadeIn();
        while (GOFade.instance.fadingIn)
        {
            yield return null;
        }

        defeatScreen.SetActive(true);
        CheckHiScore();

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            // Calculamos el progreso de la transición
            float progress = (Time.time - startTime) / duration;

            float newVolume = Mathf.Lerp(startVolume, endVolume, progress);
  
            AudioManager.instance.musicMixer.SetFloat("exposedMusicVol", newVolume);

            yield return null;
        }
        AudioManager.instance.musicMixer.SetFloat("exposedMusicVol", endVolume);


        
        
        GOFade.instance.StartFadeOut();
        while (GOFade.instance.fadingOut)
        {
            yield return null;
        }
        Time.timeScale = 0f;
        

        
        //Activar pantalla de derrota con boton de reinicio
        //Dejar de spawnear mobs
        //Frenar el temporizador
        //comprobar si es hiscore y mostrarlo
    }
    private void ShowDefeat()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(ShowDefeatCoroutine());
    }
    private void CheckHiScore()
    {
        string prevScoreMin = PlayerPrefs.GetString("HiScoreMin", "00");
        string prevScoreSec = PlayerPrefs.GetString("HiScoreSec", "00");

        //int newScore = int.Parse(ScoreTimer.instance.timerText.text.Replace(":",""));
        string[] timeSplit = ScoreTimer.instance.timerText.text.Split(':');

        lastRecord.text = prevScoreMin + ":" + prevScoreSec;//muestro el last record
        hiScoreText.text = timeSplit[0] + ":" + timeSplit[1];//muestro el actual

        if (int.Parse(timeSplit[0]) > int.Parse(prevScoreMin))
        {
            StartCoroutine(Parpadear());
            DataManager.instance.HiScoreData(timeSplit[0], timeSplit[1]);
        }
        else if (int.Parse(timeSplit[0]) == int.Parse(prevScoreMin))
        {
            if (int.Parse(timeSplit[1]) > int.Parse(prevScoreSec))
            {
                StartCoroutine(Parpadear());
                DataManager.instance.HiScoreData(timeSplit[0], timeSplit[1]);
            }
        }
    }
    private IEnumerator Parpadear()
    {
        float startTime = Time.realtimeSinceStartup;
        bool visible = true;

        while (true)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;//forma de contar el tiempo cuando el Time.timescale esta en 0

            if (visible)
            {
                Debug.Log("parpadeo");
                flashingNew.SetActive(true);
            }
            else
            {
                flashingNew.SetActive(false);
            }

            visible = !visible;
            startTime = Time.realtimeSinceStartup;

            // Esperar hasta que haya pasado el tiempo de flashEnter o flashExit
            float waitTime = visible ? flashEnter : flashExit;
            while (Time.realtimeSinceStartup - startTime < waitTime)
            {
                yield return null;
            }
        }
    }
}

