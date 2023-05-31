using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    public float attackRadius = 1f;
    public int attackDamage = 10;
    public Vector2 attackOffset = new Vector2(0f, 0f);

    private Animator animator;
    private CircleCollider2D attackCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        attackCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPos = (Vector2)transform.position + attackOffset;
        Gizmos.DrawWireSphere(attackPos, attackRadius);
    }

    void OnValidate()
    {
        attackCollider.offset = attackOffset;
        attackCollider.radius = attackRadius;
    }
}
