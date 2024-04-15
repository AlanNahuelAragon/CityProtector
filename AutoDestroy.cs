using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float tiempoDeVida = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirPrefab", tiempoDeVida);
    }

    void DestruirPrefab()
    {
        // Destruir el prefab al que está adjunto este script
        Destroy(gameObject);
    }
}
