using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int> damagableHit;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive;
    public bool IsAlive { get => _isAlive; private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.IsAlive, _isAlive);
            Debug.Log("IsAlive: " + _isAlive);
        }
    }

    public bool LockVec
    {
        get
        {
            return animator.GetBool(AnimationStrings.LockVec);
        }
        set
        {
            animator.SetBool(AnimationStrings.LockVec, value);
        }
    }


    [SerializeField]
    private bool _isInvisible = false;
    private float timeSinceHit = 0;
    public float invisibilityTimer = 0.25f;

    [SerializeField]
    private bool _isHit;

    public bool IsInvisible {
        get
        { return _isInvisible; }
        private set
        {
            _isInvisible = value;
        }
    }
    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.IsHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.IsHit, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsInvisible)
        {
            if (timeSinceHit > invisibilityTimer)
            {
                IsInvisible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

    }
    public void Hit(int damage)
    {
        if (IsAlive && !IsInvisible)
        {
            IsHit = true;
            CurrentHealth -= damage;
            damagableHit?.Invoke(damage);
            animator.SetTrigger(AnimationStrings.HitTrig);
            LockVec = true;
        }
    }

}
