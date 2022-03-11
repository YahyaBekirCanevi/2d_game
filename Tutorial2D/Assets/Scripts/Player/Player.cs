using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Health
{
    private bool hit = true;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject flash;
    [SerializeField] private Color collideColor, collideColor2;
    public override void TakeDamage(float damage)
    {
        if (hit)
        {
            StartCoroutine(PlayerHit());
            currHealth -= damage;
            healthBar.maxValue = maxHealth;
            healthBar.value = currHealth;
        }
    }
    public override void Heal(float hp)
    {
        currHealth += hp;
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            if (other.gameObject.name.Contains("Health Potion"))
            {
                this.Heal(50);
                Destroy(other.gameObject);
            }
        }
    }

    protected override IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        this.isDeath = true;
    }

    IEnumerator PlayerFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 2; i++)
        {
            sr.color = collideColor;
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 4; i++)
        {
            sr.color = collideColor2;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator PlayerHit()
    {
        FindObjectOfType<CameraShake>().ShakeItMedium();
        flash.SetActive(true);
        hit = false;
        StartCoroutine(PlayerFlash());
        yield return new WaitForSeconds(1.5f);
        hit = true;
        flash.SetActive(false);
    }
}
