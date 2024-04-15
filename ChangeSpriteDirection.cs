using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteDirection : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public float offsetAngle = 90f;
    public float rotationSpeed = 720f; // Velocidad de rotación en grados por segundo

    private void Start()
    {
        // Obtener referencia al Rigidbody2D y al SpriteRenderer
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Obtener la dirección del movimiento del Rigidbody2D
        Vector2 direction = rb2d.velocity.normalized;

        // Si la dirección no es cero, actualizar la rotación del sprite
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            spriteRenderer.transform.rotation = Quaternion.RotateTowards(spriteRenderer.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
