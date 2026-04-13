using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 5f;
    Vector2 moveInput;
    public bool IsMoving {get; private set;}

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
        rb.linearVelocity = new Vector2(moveInput.x * walkSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
    }

    void onMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

    }
}
