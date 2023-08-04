using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        controller.UpdateMoveInput(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.StartRunning();
        }
        else if (context.canceled)
        {
            controller.StopRunning();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.Jump();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.Attack();
        }
    }

    public void OnDamageBuff(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.ApplyDamageBuff();
        }
    }

    public void OnHealthBuff(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.ApplyHealthBuff();
        }
    }
    public void OnAttackSpeedBuff(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controller.ApplyAttackSpeedBuff();
        }
    }
}
