using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth = 200, currHealth = 0;
    public float deathTime = .05f;
    public bool isDeath;
    private void Start() {
        currHealth = maxHealth;
        isDeath = false;
    }
    private void Update() {
        if((currHealth <= 0 && !isDeath) || GetComponent<Rigidbody2D>().velocity.y < -20) {
            Die();
            return;
        }
        if(currHealth > maxHealth) {
            currHealth = maxHealth;
        }
    }
    public abstract void TakeDamage(float damage);
    public abstract void Heal(float hp);
    private void Die(){
        currHealth = 1;
        if(gameObject.CompareTag("Enemy")) 
            gameObject.tag = "Untagged";
        StartCoroutine(Death());
    }
    protected abstract IEnumerator Death();
}
