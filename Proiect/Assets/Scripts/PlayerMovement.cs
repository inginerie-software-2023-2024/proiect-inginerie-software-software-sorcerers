using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private BoxCollider2D collFeet;
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
    [SerializeField] public bool unlockedDash;
    [SerializeField] public bool unlockedWallJump = false;

    private bool allowed = true;
    private float dashTime = 0.2f;
    private bool dashing = false;

    [SerializeField] private bool isWallSliding = false;
    private float wallSlidingSpeed = 2f;


    [SerializeField] private Transform wallCheckLeft, wallCheckRight;
    [SerializeField] private LayerMask wallLayer;

    private enum JumpDirection {Left, Right, Ground};
    private JumpDirection NextJumpDirection;

    private enum MovementState {idle, running, jumping, falling , doubleJumping, wallJump};

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        collFeet = GetComponent <BoxCollider2D>();
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

        if(Input.GetButtonDown("Jump") &&
            (IsGrounded()|| CanWallJump() || (doubleJump && unlockedDoubleJump)) &&
            rb.bodyType == RigidbodyType2D.Dynamic)
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

        if (IsGrounded() == true)
        {
            doubleJump = true;
            NextJumpDirection = JumpDirection.Ground;
        }

        if (unlockedWallJump) WallSlide();
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

        if (isWallSliding)
        {
            state = MovementState.wallJump;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collFeet.bounds.center, collFeet.bounds.size, 0f, Vector2.down, 0.05f, jumpableGround);
    }

    private bool IsWalled()
    {
        bool left, right;
        left = Physics2D.OverlapCircle(wallCheckLeft.position, 0.075f, wallLayer);
        right =Physics2D.OverlapCircle(wallCheckRight.position, 0.075f, wallLayer);

        return left || right;
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

    }

    private bool CanWallJump()
    {
        if (!IsWalled()||!unlockedWallJump)
            return false;
        Debug.Log("Wall Jump");
        bool left;
        left = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        if (NextJumpDirection == JumpDirection.Ground)
        {
            if (left) NextJumpDirection = JumpDirection.Right;
            else NextJumpDirection = JumpDirection.Left;
            return true;
        }
        else if (NextJumpDirection == JumpDirection.Right && !left)
        {
            NextJumpDirection = JumpDirection.Left;
            return true;
        }
        else if (NextJumpDirection == JumpDirection.Left && left)
        {
            NextJumpDirection = JumpDirection.Right;
            return true;
        }
        return false;
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
