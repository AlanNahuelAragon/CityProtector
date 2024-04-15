using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento de los enemigos
    public float sideSpeed= 2f;
    public float minX = -4f; // Límite izquierdo de movimiento
    public float maxX = 4f; // Límite derecho de movimiento
    public float minY = -6f; // Límite inferior de movimiento
    public float destructionY = -10f; // Punto en el que los enemigos serán destruidos
    private bool isErratic=false;
    private Rigidbody2D rb;
    private bool isRecovering=false;

    void Start()
    {
        int rndNum = Random.Range(0, 2);
        //Debug.Log("RND: "+rndNum);
        if (rndNum == 0)
        {
            isErratic = true;
        }
        speed= Random.Range(speed,0.5f);
        rb = GetComponent<Rigidbody2D>();
        // Iniciar el movimiento aleatorio
        rb.velocity = new Vector2(Random.Range(-1f, 1f), -speed);
        if(isErratic){
            InvokeRepeating("ChangeDir",0f,2f);
        }
        
    }

    void Update()
    {
        // Si el enemigo alcanza el punto de destrucción, destruirlo
        if (transform.position.y <= destructionY)
        {
            Destroy(gameObject);
        }

        // Si el enemigo llega al límite izquierdo o derecho, cambiar su dirección
        if (!isRecovering)
        {
            if (transform.position.x <= minX || transform.position.x >= maxX)
            {
                StartCoroutine(InvertDir());
            }
        }

    }
    IEnumerator InvertDir()
    {
        //Debug.Log("Recuperandome");
        isRecovering = true;
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        yield return new WaitForSeconds(2f);
        isRecovering = false;
    }
    void ChangeDir(){
        if (!isRecovering) 
        {
            //Debug.Log("Puedo Cambiar");
            rb.velocity = new Vector2(Random.Range(-1f, 1f), -speed);
        }
        
    }
}
