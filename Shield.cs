using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    //Blink
    public SpriteRenderer spriteRenderer;
    public Material flashMaterial;
    public float flashDuration = 1f;

    private Material defaultMaterial;
    private bool isFlashing = false;
    //Lives
    public int shieldLife = 3;
    public GameObject spark;
    public Text shieldLifeText;
    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = spriteRenderer.material;
        shieldLifeText.text = shieldLife.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashSprite());
            }
            shieldLife = shieldLife - 1;
            shieldLifeText.text = shieldLife.ToString();
            GameObject newSpark = Instantiate(spark, other.transform.position, other.transform.rotation);
            if (shieldLife < 1)
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator FlashSprite()
    {
        isFlashing = true;

        // Cambiamos el material del sprite al material de destello
        spriteRenderer.material = flashMaterial;

        // Esperamos el tiempo especificado
        yield return new WaitForSeconds(flashDuration);

        // Restauramos el material original del sprite
        spriteRenderer.material = defaultMaterial;

        isFlashing = false;
    }
}
