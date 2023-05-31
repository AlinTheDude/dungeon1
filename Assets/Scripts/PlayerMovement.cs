using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private bool isJumping = false;

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool wasJumping = isJumping;
        isJumping = !IsGrounded();

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        
        if (wasJumping && !isJumping)
        {
            UpdateAnimationState();
        }
        else
        {
            UpdateAnimationState();
        }

    }
   

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return hit.collider != null;
    }

    private void UpdateAnimationState()
    {
        MovementState stato;
        if (dirX > 0f)
        {
            stato = MovementState.running;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            stato = MovementState.running;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            stato = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            stato = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            stato = MovementState.falling;
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Attack", true);
        }

        anim.SetInteger("stato", (int)stato);
    }
    
    public void Attack()
    {
        Collider2D[] Enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in Enemy)
        {
            Debug.Log("Hit enemy");
            enemyGameobject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    public void EndAttack()
    {
        anim.SetBool("Attack", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

}



