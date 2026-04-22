using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airSpeed = 4f;
    Vector2 moveInput;
    TouchingDirection touchingDirection;

    public float jumpForce = 10f;

    public float CurrentSpeed
    {
        get
        {
            if (CanMove)
            {
                if (touchingDirection.IsGrounded)
                {
                    if (IsMoving && !touchingDirection.IsOnWall)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else if (IsMoving)
                        {
                            return walkSpeed;
                        }
                    }
                }
                else
                {
                    if (IsMoving && !touchingDirection.IsOnWall)
                        return airSpeed;
                }
                return 0f;
            }
            else
            {
                return 0f;
            }
        }
    }

    [SerializeField]
    private Boolean _isMoving = false;
    public bool IsMoving { get { return _isMoving; } 
        private set { _isMoving = value; 
            animator.SetBool(AnimationStrings.IsMoving, _isMoving); } }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning { get { return _isRunning; } 
        private set { _isRunning = value; 
            animator.SetBool(AnimationStrings.IsRunning, _isRunning); }
    }

    Animator animator;

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentSpeed, rb.linearVelocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDir(moveInput);

    }

    private void SetFacingDir(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context){
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Check if alive
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.Jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.Attack);
        }
    }
}
