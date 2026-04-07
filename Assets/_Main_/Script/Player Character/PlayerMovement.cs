using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 0.5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    bool isGrounded;
    bool isDashing;
    float dashTimer;
    float dashCooldownTimer;
    PlayerControls controls;
    Vector2 moveInput;
    Vector2 lastMoveDirection;


    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Dash.performed += ctx => StartDash();
        controls.Player.Jump.performed += ctx => Jump();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        dashCooldownTimer -= Time.fixedDeltaTime;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isDashing)
        {
            body.linearVelocity = new Vector2(lastMoveDirection.x * dashSpeed, body.linearVelocity.y);
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
            return;
        }

        body.linearVelocity = new Vector2(moveInput.x * speed, body.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

        if (moveInput.x != 0)
        {
            lastMoveDirection = moveInput;
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    void StartDash()
    {
        if (dashCooldownTimer <= 0 && moveInput != Vector2.zero)
        {
            isDashing = true;
            dashTimer = dashTime;
            dashCooldownTimer = dashCooldown;
            animator.SetTrigger("Dash");
        }
    }

    void Jump()
    {
        if (isGrounded && !isDashing)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }
}
