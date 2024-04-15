using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private int enemyType=0;
    public Transform spawnPoint;
    public float spawnInterval;
    public float waveInterval;//cada se acelera el intervalo de spawn

    //timer
    public float timer;
    public float waveTimer;//Timer de oleada

    public bool isTimeRunning = true;
    void Start()
    {
        StartCoroutine(Timer());
    }

    void SpawnEnemy(){
        enemyType= Random.Range(0,enemyPrefab.Length);
        //Debug.Log("type "+enemyType);
        GameObject newEnemy = Instantiate(enemyPrefab[enemyType],spawnPoint.position,spawnPoint.rotation);
    }

    IEnumerator Timer()
    {
        while (isTimeRunning)
        {
            timer += 0.1f;
            waveTimer += 0.1f;
            if (timer>=spawnInterval)
            {
                timer = 0;
                SpawnEnemy();
            }
            if (waveTimer >= waveInterval)
            {
                waveTimer = 0;
                spawnInterval -= 0.1f;
            }


            yield return new WaitForSeconds(0.1f);

        }
        
    }
}
