using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab del efecto de explosión
    public GameObject spark;
    public int life = 0;
    public string weakTo;
    public int lightDmg;
    public int heavyDmg;
    public int pemDmg;

    //Blink-----
    public SpriteRenderer spriteRenderer;
    public Material flashMaterial;
    public float flashDuration = 1f;

    private Material defaultMaterial;
    private bool isFlashing = false;

    private void Start()
    {
        defaultMaterial = spriteRenderer.material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LightShoot"))
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashSprite());
            }
            GameObject newExplosion = Instantiate(spark, other.transform.position, other.transform.rotation);
            life = life-lightDmg;
            AudioManager.instance.PlayAudioPitch(AudioManager.instance.hit);
            if (life < lightDmg)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.lexplosion);
                DestroyEnemy();
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HeavyShoot"))
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashSprite());
            }
            GameObject newExplosion = Instantiate(spark, other.transform.position, other.transform.rotation);
            life = life - heavyDmg;
            AudioManager.instance.PlayAudioPitch(AudioManager.instance.hit);
            if (life < 1)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.lexplosion);
                DestroyEnemy();
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PemShoot"))
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashSprite());
            }
            GameObject newExplosion = Instantiate(spark, other.transform.position, other.transform.rotation);
            life = life - pemDmg;
            AudioManager.instance.PlayAudioPitch(AudioManager.instance.hit);
            if (life < 1)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.lexplosion);
                DestroyEnemy();
            }
            
        }
        if (other.CompareTag("Shields"))
        {
            DestroyEnemy();
        }
    }
    private void DestroyEnemy()
    {
        AudioManager.instance.PlayAudioPitch(AudioManager.instance.lexplosion);
        GameObject newExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
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

