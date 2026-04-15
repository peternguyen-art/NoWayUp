using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentSpeed
    {
        get
        {
            if (IsMoving && IsRunning)
            {
                return runSpeed;
            }
            else if (IsMoving)
            {
                return walkSpeed;
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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentSpeed, rb.linearVelocity.y);
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
}
