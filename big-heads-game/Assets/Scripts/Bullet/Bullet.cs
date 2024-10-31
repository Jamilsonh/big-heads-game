using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Vector2 direction;

    public void SetDirection(Vector2 targetPosition) {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Update() {
        // Move o projétil na direção definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Aqui você pode adicionar lógica para o projétil causar dano, desaparecer, etc.
        Debug.Log("Hit: " + collision.name);
        //Destroy(gameObject); // Destroi o projétil ao colidir com algo
    }
}
