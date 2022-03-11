using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAncientLaser : EnemyAction
{
    [SerializeField] protected float attackDistance = 2;
    [SerializeField] protected Transform attackPlace;
    protected override void Patrol() {
        switch(direction) {
            case -1 : {
                if(transform.position.x > minX)
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                else direction = 1;
                break;
            }
            case 1 : {
                if(transform.position.x < maxX)
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                else direction = -1;
                break;
            }
            default : break;
        }
        xScale = rb.velocity.x > 0 ? 1 : -1;
    }
    protected override void Attack() {
        Vector3 playerDir = (playerPos.position - transform.position).normalized;
        xScale = playerDir.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(0, rb.velocity.y);
        Collider2D attackPlayer = Physics2D.OverlapCircle(attackPlace.position, attackDistance);
        if(attackPlayer != null) if(attackPlayer.CompareTag("Player")) 
                if(!attackPlayer.gameObject.GetComponent<Health>().isDeath){
                    attackPlayer.gameObject.GetComponent<Health>().TakeDamage(damage);
                    attackTimer = Time.time;
                }
    }

    protected override bool IsAttacking()
    {
        return Vector3.Distance(transform.position, playerPos.position) <= detectDistance;
    }
}
