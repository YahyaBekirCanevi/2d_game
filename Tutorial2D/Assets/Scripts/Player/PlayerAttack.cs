using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking;
    public string animationName = "";
    private bool activeToReset;
    private bool availableToCombo = true;
    private int combo = -1;
    public Vector3 attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public float damage;
    private void Update() {
        SwordAttack();
        if(!isAttacking) ResetCombo();
    }
    private void SwordAttack(){
        if(Input.GetKeyDown(KeyCode.E) && availableToCombo){
            if(combo < 3){
                if(combo < 2)combo++;
                animationName = AttackAnimation();
                isAttacking = true;
                activeToReset = true;
                availableToCombo = false;

                Collider2D[] attackEnemies = Physics2D.OverlapCircleAll(transform.position+attackPos, attackRange, enemyLayer);
                foreach(Collider2D enemy in attackEnemies){
                    if(!enemy.gameObject.GetComponent<Health>().isDeath)
                        enemy.gameObject.GetComponent<Health>().TakeDamage(damage);
                }
            }
            else {
                isAttacking = false;
            }
        }
    }
    private void MakeAvailableToCombo() => availableToCombo = true;
    private void ResetCombo() => combo = -1;
    private void ResetComboState() {
        if(activeToReset) {
            isAttacking = false;
            activeToReset = false;
            availableToCombo = true;
        }
    }
    private string AttackAnimation(){
        string animation = "";
        switch(combo){
            case 0: {
                animation = "Attack1";
                break;
            }
            case 1: {
                animation = "Attack2";
                break;
            }
            case 2: {
                animation = "Attack3";
                break;
            }
            default: {
                animation = "Idle";
                isAttacking = false;
                break;
            }
        }
        return animation;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+attackPos, attackRange);
    }
}
