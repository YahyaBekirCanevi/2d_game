using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    [Header("Movement")]
    #region Movement
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float distance;
    protected float minX, maxX;
    protected float xScale = 1;
    [SerializeField] protected int direction;
    [SerializeField] protected State state;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Health health;
    protected Transform playerPos;
    #endregion
    [Header("Attack")]
    #region Attack
    [SerializeField] protected float detectDistance = 2;
    protected float idleDuration = 1.5f, attackTimer = 0;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected int damage;
    [SerializeField] protected AnimationHandle animationHandle;
    #endregion
    private void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        playerPos = GameObject.Find("Knight").transform;
        maxX = transform.position.x + (distance/2);
        minX = maxX - distance;
    }
    private void Update() {
        animationHandle.Action = () => state = State.Idle;
        if(state != State.Attack)
            state = (health.isDeath || Time.time - attackTimer < idleDuration) ? State.Idle : IsAttacking() ? State.Attack : State.Patrol;
        state = GameObject.Find("Knight").GetComponent<Health>().isDeath ? State.Patrol : state;
        anim.Play(health.isDeath ? "Death" : state == State.Attack ? "Attack" : "Idle");
    }
    private void FixedUpdate() {
        if(!health.isDeath) {
            switch(state){
                case State.Patrol : {
                    Patrol();
                    break;
                }
                case State.Attack : {
                    Attack();
                    break;
                }
                default : break;
            }
            transform.localScale = new Vector2(xScale, transform.localScale.y);
            rb.isKinematic = false;
            GetComponent<Collider2D>().enabled = true;
        }
        else {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            return;
        }
        
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
    protected abstract bool IsAttacking();
    protected abstract void Patrol();
    protected abstract void Attack();
    protected enum State { Idle, Patrol, Attack }
}
