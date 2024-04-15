using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //--------Controls
    public bool leftButton = false;
    public bool rightButton = false;
    public bool fireButton = false;
    //---------Cannon
    public Transform shootPoint;
    public Transform cannonTransform; // Referencia al transform del cañón que queremos rotar
    public GameObject projectilePrefab; // Prefab del proyectil que dispararemos
    public float minRotation = -90f;
    public float maxRotation = 90f;

    public float rotationSpeed = 100f; // Velocidad de rotación del cañón
    //public float projectileSpeed = 10f; // Velocidad de los proyectiles
    public float shootCooldown = 0.1f; // Tiempo entre cada disparo

    private float shootTimer; // Temporizador para controlar la cadencia de disparo
    //---------------Shoot
    public float chargeTimeThreshold1 = 1.5f;
    public float chargeTimeThreshold2 = 3f;
    public GameObject projectilePrefabNormal;
    public GameObject projectilePrefabSecondary;
    public GameObject projectilePrefabTertiary;
    //public Transform spawnPoint;
    private bool isCharging = false;
    private float chargeStartTime;
    public Slider chargeSlider;
    //Muzzle
    public GameObject muzzleLight;
    public GameObject muzzleHeavy;
    public GameObject muzzlePem;

    // Update is called once per frame
    void Update()
    {
    {
        // Actualizar el valor de la barra de progreso mientras se mantiene presionado el botón
        if (isCharging)
        {
            float chargeTime = Time.time - chargeStartTime;
            float progress = Mathf.Clamp01(chargeTime / chargeTimeThreshold2); // Limitar el valor entre 0 y 1
            chargeSlider.value = progress; // Actualizar el valor de la barra de progreso
        }
    }
        // Rotar el cañón hacia la izquierda cuando se presiona el botón izquierdo
        if (leftButton)
        {
            
            cannonTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            
        }

        // Rotar el cañón hacia la derecha cuando se presiona el botón derecho
        if (rightButton)
        {
        
            cannonTransform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

        }

        // Disparar cuando se presiona el botón de disparo
        if (fireButton)
        {
            //Shoot();
        }
        LimitRot();
        // Actualizar el temporizador de disparo
        shootTimer -= Time.deltaTime;
    }

    public void LeftDown()
    {
        leftButton = true;
        rightButton = false;
    }
    public void LeftUp()
    {
        leftButton = false;
    }
    public void RightDown()
    {
        leftButton = false;
        rightButton = true;
    }
    public void RightUp()
    {
        rightButton = false;
    }
    /*
    public void FireDown()
    {
        fireButton = true;
    }
    public void FireUp()
    {
        fireButton = false;
    }
    
    void Shoot()
    {
        // Verificar si el temporizador ha terminado y podemos disparar nuevamente
        if (shootTimer <= 0)
        {
            // Instanciar un nuevo proyectil
            GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            // Aplicar velocidad al proyectil
            rb.velocity = shootPoint.up * projectileSpeed;

            // Reiniciar el temporizador de disparo
            shootTimer = shootCooldown;
        }
    }*/
    public void ChargingShoot(){
        isCharging = true;
        chargeStartTime = Time.time;
    }
    public void ShootCharged(){
        // Si se estaba cargando el disparo
        if (isCharging)
        {
            chargeSlider.value=0;
            isCharging = false;
            // Calcular el tiempo de carga
            float chargeTime = Time.time - chargeStartTime;
            CannonRecoil.instance.ActivateRecoil();
            // Determinar qué proyectil disparar según el tiempo de carga
            if (chargeTime < chargeTimeThreshold1)
            {
                ShootProjectile(projectilePrefabNormal,muzzleLight,5f);
                AudioManager.instance.PlayAudio(AudioManager.instance.lshoot);
            }
            else if (chargeTime < chargeTimeThreshold2)
            {
                ShootProjectile(projectilePrefabSecondary,muzzleHeavy,20f);
                AudioManager.instance.PlayAudio(AudioManager.instance.hshoot);
            }
            else
            {
                ShootProjectile(projectilePrefabTertiary,muzzlePem, 15f);
                AudioManager.instance.PlayAudio (AudioManager.instance.pshoot);
            }
        }
    }
    private void ShootProjectile(GameObject projectilePrefab,GameObject muzzlePrefab, float projectileSpeed){
        // Instanciar un nuevo proyectil
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        GameObject newMuzzle = Instantiate(muzzlePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        // Aplicar velocidad al proyectil
        rb.velocity = shootPoint.up * projectileSpeed;

        // Reiniciar el temporizador de disparo
        shootTimer = shootCooldown;
    }

    private void LimitRot(){
        Vector3 objectEulerAngles = cannonTransform.rotation.eulerAngles;
        objectEulerAngles.z=(objectEulerAngles.z > 180)? objectEulerAngles.z -360: objectEulerAngles.z;
        objectEulerAngles.z= Mathf.Clamp(objectEulerAngles.z,minRotation,maxRotation);
        cannonTransform.rotation= Quaternion.Euler(objectEulerAngles);
    }

}
