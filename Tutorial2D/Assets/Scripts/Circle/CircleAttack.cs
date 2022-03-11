using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float damage;
    [SerializeField] private float attackDuration = 3;
    [SerializeField] private float attackTimer = 0;
    [SerializeField] private GameObject ammo;
    [SerializeField] private LayerMask playerLayer;
    void Update()
    {
        //Debug.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Color.blue);
        if(Time.time - attackTimer >= attackDuration) {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackDistance, playerLayer);
            foreach(Collider2D col in cols) 
                if(col.CompareTag("Player")) 
                    if(!col.gameObject.GetComponent<Health>().isDeath){
                        ammo.GetComponent<CircleAmmo>().player = col.gameObject;
                        ammo.GetComponent<CircleAmmo>().damage = damage;
                        Instantiate(ammo, transform.position, Quaternion.identity);
                        attackTimer = Time.time;
                    }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
