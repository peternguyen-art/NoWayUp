using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 5f;
    Vector2 moveInput;

    [SerializeField]
    private Boolean _isMoving = false;
    public bool IsMoving { get { return _isMoving; } 
        private set { _isMoving = value; 
            animator.SetBool("IsMoving", _isMoving); } }

    [SerializeField]
    private bool isRunning = false;
    public bool IsRunning { get { return isRunning; } 
        private set { isRunning = value; 
            animator.SetBool("IsRunning", isRunning); }
    }


    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

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
