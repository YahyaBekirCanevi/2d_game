using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAmmo : MonoBehaviour
{
    public float damage;
    public float force;
    public bool isAttacking = false;
    private Rigidbody2D rb;
    public GameObject player;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(!isAttacking){
            Vector2 playerPos = player.transform.position;
            Vector2 dir = (playerPos - (Vector2)transform.position).normalized;
            Debug.DrawRay(FindObjectOfType<CircleAttack>().transform.position, dir * force);
            rb.velocity = dir * force;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * angle;
            isAttacking = true;
            StartCoroutine(DestroyAmmo());
        }
    }
    IEnumerator DestroyAmmo(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.collider.gameObject.CompareTag("Enemy") && !other.collider.gameObject.CompareTag("Untagged")) {
            if(other.gameObject == player)
                player.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
