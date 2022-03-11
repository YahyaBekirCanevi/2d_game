using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{

    public override void TakeDamage(float damage)
    {
        FindObjectOfType<CameraShake>().ShakeItLow();
        currHealth -= damage;
        /* healthBar.maxValue = maxHealth;
        healthBar.value = currHealth; */
    }
    public override void Heal(float hp)
    {
        currHealth += hp;
        /* healthBar.maxValue = maxHealth;
        healthBar.value = currHealth; */
    }
    
    protected override IEnumerator Death() {
        yield return new WaitForSeconds(deathTime);
        isDeath = true;
    }
}
