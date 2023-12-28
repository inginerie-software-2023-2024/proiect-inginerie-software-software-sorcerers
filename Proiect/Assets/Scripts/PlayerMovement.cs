using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private TrailRenderer trail;
    public bool unlockedDoubleJump;
    private bool doubleJump;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private bool unlockedDash;
    private bool allowed = true;
    private float dashTime = 0.2f;
    private bool dashing = false;



    private enum MovementState {idle, running, jumping, falling , doubleJumping};

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (dashing)
            return;

        dirX = Input.GetAxisRaw("Horizontal");
        if(rb.bodyType == RigidbodyType2D.Dynamic)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && (IsGrounded() || (doubleJump && unlockedDoubleJump)) && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            if(IsGrounded() == false)
            {
                doubleJump = false;
            }else
            {
                doubleJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && allowed && unlockedDash)
        {
            StartCoroutine(Dash());
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
            if(doubleJump == false)
            {
                state = MovementState.doubleJumping;
            }
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private IEnumerator Dash()
    {
        float originalGravity = rb.gravityScale;
        dashing = true;
        allowed = false;
        rb.gravityScale = 0f;
        if (dirX != 0f)
            rb.velocity = new Vector2(dashSpeed * dirX, 0f);
        else if (sprite.flipX == false)
            rb.velocity = new Vector2(dashSpeed, 0f);
        else
            rb.velocity = new Vector2(-dashSpeed, 0f);
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        rb.gravityScale = originalGravity;
        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        allowed = true;
    }

}
