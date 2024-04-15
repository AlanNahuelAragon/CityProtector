using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public bool move=false;
    public float speedx = 0f;
    public float speedy = 0f;
    public bool rotate=false;
    public float rotSpeed=0f;
    
    public bool change=false;
    public float secondsToChange = 1f;

    public string changedir = "";//x,y,both
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        if (move)
        {
            rb.velocity = new Vector2(speedx, speedy);
        }
        if (change)
        {
            StartCoroutine(TimeToChange());
        }
    }

    private void Update()
    {
        if (rotate) 
        {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }

    private void ChangeDirection()
    {

        switch (changedir)
        {
            case "x":
                Debug.Log("x");
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                break;
            case "y":
                Debug.Log("y");
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                break;
            case "both":
                Debug.Log("both");
                rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
                break;
        }

    }

    IEnumerator TimeToChange()
    {
        while (change)
        {
            Debug.Log("loop");
            yield return new WaitForSeconds(secondsToChange);
            ChangeDirection();
        }
        
       
    }
}
