using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = .001f;
    public bool right;
    public float damage;
    private void Update() {
        transform.Translate(Vector2.right * (right ? 1 : -1) * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        if(!other.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
