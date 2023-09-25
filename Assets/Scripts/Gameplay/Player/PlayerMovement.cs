using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    public Vector2 lastMoveDirection = Vector2.zero;
    private Rigidbody2D rb = null;
    public float moveSpeed = 5;
    private Animator anim;

    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;

    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        UpdateAnimation();
        lastMoveDirection = moveVector.normalized;
    }
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        anim.SetBool("idle", true);
    }
    private void UpdateAnimation()
    {
        anim.SetBool("idle", false);
        if (moveVector == new Vector2(0, 1))
        {
            anim.SetTrigger("walkUp");
        }
        else if (moveVector == new Vector2(1, 0))
        {
            anim.SetTrigger("walkRight");
        }
        else if (moveVector == new Vector2(0, -1))
        {
            anim.SetTrigger("walkDown");
        }
        else if (moveVector == new Vector2(-1, 0))
        {
            anim.SetTrigger("walkLeft");
        }

    }

}
