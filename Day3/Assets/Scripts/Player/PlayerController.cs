using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingObject))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 8f;
    public int AttackSpeedMulti = 1;
    private Vector2 moveInput;

    private bool isRunning = false;
    public bool isAttacking = false;

    [HideInInspector] private Rigidbody2D rb;
    [HideInInspector] private Animator anim;
    private TouchingObject touchingDirections;
    private Health health;

    #region Properties
    private float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && touchingDirections.IsGrounded)
                {
                    if (isRunning) return runSpeed;
                    else return walkSpeed;
                }
                return 0;
            }
            return 0;
        }
    }

    private bool IsMoving { get => moveInput != Vector2.zero; }

    private bool CanMove { get => anim.GetBool(AnimationString.canMove); }
    #endregion

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingObject>();
        health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        float horizontal = moveInput.x;
        rb.velocity = new Vector2(horizontal * CurrentMoveSpeed, rb.velocity.y);
        Flip(horizontal);
    }

    #region Controller
    public void UpdateMoveInput(Vector2 input)
    {
        moveInput = input;
        bool isMoving = moveInput != Vector2.zero;
        anim.SetBool(AnimationString.isMoving, isMoving);
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetBool(AnimationString.isRunning, true);
    }

    public void StopRunning()
    {
        isRunning = false;
        anim.SetBool(AnimationString.isRunning, false);
    }

    public void Jump()
    {
        if (touchingDirections.IsGrounded && CanMove)
        {
            anim.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void Attack()
    {
        anim.SetInteger(AnimationString.attackIndex, Random.Range(0, 2));
        if (CanMove)
            anim.SetTrigger(AnimationString.attackTrigger);
    }
    public void ApplyDamageBuff()
    {
        if (CanMove)
        {
            health.BuffDamage();
            anim.SetInteger(AnimationString.buffIndex, 0);
            anim.SetTrigger(AnimationString.buffTrigger);
        }
    }


    public void ApplyHealthBuff()
    {
        if (CanMove)
        {
            health.BuffHealth();
            anim.SetInteger(AnimationString.buffIndex, 1);
            anim.SetTrigger(AnimationString.buffTrigger);
        }
    }

    private bool isBuffed = false;

    public void ApplyAttackSpeedBuff()
    {
        if (CanMove && !isBuffed)
        {
            isBuffed = true;
            StartCoroutine(BuffDuration());
        }
    }

    private IEnumerator BuffDuration()
    {
        int baseSpeed = AttackSpeedMulti;
        AttackSpeedMulti *= 3;

        anim.SetInteger(AnimationString.buffIndex, 2);
        anim.SetTrigger(AnimationString.buffTrigger);
        anim.SetFloat(AnimationString.attackSpeedMulti, AttackSpeedMulti);

        yield return new WaitForSeconds(5f);

        AttackSpeedMulti = baseSpeed;
        anim.SetFloat(AnimationString.attackSpeedMulti, AttackSpeedMulti);
        isBuffed = false;
    }
    #endregion
    private void Flip(float h)
    {
        if (h > 0)
        {
            transform.localScale = Vector2.one;
            health.hpBar.fillOrigin = 1;
        }
        else if (h < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            health.hpBar.fillOrigin = 0;
        }
    }
}
