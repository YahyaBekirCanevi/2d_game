using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] [Range(0, 1)] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Animator anim;
    private PlayerAttack playerAttack;
    private Health health;
    private float moveInput;
    private bool facingRight = true;
    private bool isJumping = true;
    [SerializeField] private Vector3 range;
    public Color collideColor, collideColor2;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        health.deathTime = 1.2f;
    }

    void FixedUpdate()
    {
        if (health.isDeath)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            anim.Play("Death");
            return;
        }
        else
        {
            CheckCollisionForJump();
            Movement();
            rb.isKinematic = false;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveInput = playerAttack.isAttacking ? 0 : horizontal * runSpeed;
        anim.Play(playerAttack.isAttacking ? playerAttack.animationName :
            (isJumping ? (rb.velocity.y > 0 ? "Jump" : "Fall")
            : (moveInput != 0 ? "Run" : "Idle")));

        rb.velocity = new Vector2(moveInput, rb.velocity.y);
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeight);
        if (!facingRight && moveInput > 0 || facingRight && moveInput < 0)
            Flip();
    }
    private void CheckCollisionForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);
        if (bottomHit != null)
        {
            if (bottomHit.gameObject.CompareTag("Ground") && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
            }
            else isJumping = false;
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        playerAttack.attackPos.x *= playerAttack.attackPos.x > 0 ? facingRight ? 1 : -1 : facingRight ? -1 : 1;
        GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, range);
    }
}
