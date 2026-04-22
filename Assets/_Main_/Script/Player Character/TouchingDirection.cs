using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallCheckDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator anim;

    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded { get { return _isGrounded; } 
        private set { 
            _isGrounded = value; 
            anim.SetBool(AnimationStrings.IsGrounded, value);
        }  }

    [SerializeField]
    private bool _isOnWall = true;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            anim.SetBool(AnimationStrings.IsOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling = true;
    private Vector2 wallCheckDir => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
            anim.SetBool(AnimationStrings.IsOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDir, contactFilter, wallHits, wallCheckDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
