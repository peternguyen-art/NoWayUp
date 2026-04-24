using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchingDirection))]
[RequireComponent(typeof(Damagable))]

public class Enemy : MonoBehaviour
{
    public float walkSpeed = 2f;
    public DetectionZone playerDetectionZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    Damagable damagable;

    public enum WalkDirection
    {
        Left,
        Right
    }

    private WalkDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkDirection walkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkDirection.Right)
                {
                    walkDirectionVector = Vector2.left;
                } else if (value == WalkDirection.Left)
                {
                    walkDirectionVector = Vector2.right;
                }
            }
                _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.HasTarget, _hasTarget);
        }
    }

    public bool CanMove
    {
        get => animator.GetBool(AnimationStrings.CanMove);
    }
    public float AttackCooldown {
        get
        {
            return animator.GetFloat(AnimationStrings.AttackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.AttackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damagable = GetComponent<Damagable>();
    }

    private void Update()
    {
        HasTarget = playerDetectionZone.detectedColliders.Count > 0;
        if (AttackCooldown>0)
        {
            AttackCooldown -= Time.deltaTime;

        }
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsOnWall && touchingDirection.IsGrounded || cliffDetectionZone.detectedColliders.Count==0)
        {
            FlipDir();
        }

        if (!damagable.LockVec)
        {if (CanMove)
            {
                rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
    }

    private void FlipDir()
    {
        if (walkDirection == WalkDirection.Left)
        {
            walkDirection = WalkDirection.Right;
        }
        else if (walkDirection == WalkDirection.Right)
        {
            walkDirection = WalkDirection.Left;
        }
        else
        {
            Debug.LogError("Enemy is on wall but walk direction is not left or right");
        }
    }

    public void OnHit(int damage)
    {
        damagable.Hit(damage);
    }
}
