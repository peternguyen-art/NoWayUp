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
        if (isDashing)
        {
            body.linearVelocity = moveInput.normalized * dashSpeed;
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
            return;
        }
        body.linearVelocity = moveInput * speed;
        animator.SetFloat("Speed", moveInput.magnitude);

        if (lastMoveDirection != Vector2.zero)
        {
          lastMoveDirection = moveInput;
        };

        if (moveInput.x > 0)
        spriteRenderer.flipX = false;

        if (moveInput.x < 0)
        spriteRenderer.flipX = true;
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
}
    