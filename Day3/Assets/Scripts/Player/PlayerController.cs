using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingObject))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 8f;
    Vector2 moveInput;
    TouchingObject touchingDirections;
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && touchingDirections.IsGrounded)
                {
                    if (IsRunning) return runSpeed;
                    else return walkSpeed;
                }
                return 0;
            }
            return 0;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            anim.SetBool(AnimationString.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;
            anim.SetBool(AnimationString.isRunning, value);
        }
    }

    public bool CanMove { get => anim.GetBool(AnimationString.canMove); }

    Rigidbody2D rb;
    Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingObject>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * CurrentMoveSpeed, rb.velocity.y);
        Flip(horizontal);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
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
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            anim.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetInteger(AnimationString.attackIndex, Random.Range(0, 2));
            anim.SetTrigger(AnimationString.attackTrigger);
        }
    }

    private void Flip(float h)
    {
        if (h > 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (h < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
