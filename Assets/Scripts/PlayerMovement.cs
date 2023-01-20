using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle, running, jumping, falling};

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(dirX > 0f) 
        {
            state = MovementState.running;
            sprite.flipX = false;
        }

        else if(dirX < 0f) 
        {
            state = MovementState.running;
            sprite.flipX = true;
        }

        else 
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }

        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
            
        }

        anim.SetInteger("state", (int)state);

    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    //adding comment to test git push
    //second test
}
