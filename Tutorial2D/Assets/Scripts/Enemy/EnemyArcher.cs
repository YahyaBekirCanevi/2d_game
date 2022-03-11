using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : EnemyAction
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowPos;
    protected override void Patrol() {
        Vector3 a = arrowPos.position;
        a.x *= xScale;
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
        Collider2D attackPlayer = Physics2D.OverlapCircle(arrowPos.position, detectDistance, playerLayer);
        if(attackPlayer != null) if(attackPlayer.CompareTag("Player")) 
                if(!attackPlayer.gameObject.GetComponent<Health>().isDeath){
                    attackPlayer.gameObject.GetComponent<Health>().TakeDamage(damage);
                    attackTimer = Time.time;
                }
    }
    private void FireArrow() {
        Vector3 a = arrowPos.position;
        a.x *= xScale;
        arrow.GetComponent<Arrow>().right = xScale < 0;
        arrow.GetComponent<Arrow>().damage = damage;
        Instantiate(arrow, a+transform.position, Quaternion.identity);
    }

    protected override bool IsAttacking() {
        Debug.DrawRay(arrowPos.position, Vector2.right * (xScale > 0 ? 1 : -1) * detectDistance, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(arrowPos.position, Vector2.right * (xScale > 0 ? 1 : -1), detectDistance, playerLayer);

        return hit.collider ? hit.collider.gameObject.CompareTag("Player") : false;
    }
}
