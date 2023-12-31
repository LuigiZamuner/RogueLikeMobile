using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    public Vector2 lastMoveDirection = Vector2.zero;
    private Rigidbody2D rb = null;
    private Health playerHealth;
    private TrailRenderer tr;
    private SpriteRenderer sr;
    public float moveSpeed = 5;
    private Animator anim;
    public bool isDashing = false;
    private bool canDash = true;
    private float dashingPower = 15f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 1.5f;

    public int animationIndex = 0; //0= idle 1= up 2= down 3= right 4= left
    public int lastAnimationIndex = 1;
    private PlayerTouchMovement touchMovement;
    private void Awake()
    {

        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Health>();

        // Obtenha uma refer�ncia ao PlayerTouchMovement
        touchMovement = GetComponent<PlayerTouchMovement>();

    }
    private void Start()
    {
        tr.emitting = false;
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Dash.started += OnDashStarted;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        input.Player.Dash.started -= OnDashStarted;


    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            // Verifique se h� entrada do joystick
            if (touchMovement != null)
            {
                moveVector = touchMovement.movementAmount;
                UpdateAnimation();
            }

            rb.velocity = moveVector * moveSpeed;
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        lastMoveDirection = moveVector.normalized;
    }
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        anim.SetBool("idle", true);
    }
    private void OnDashStarted(InputAction.CallbackContext value)
    {
        if (canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    public void CallDashButton()
    {
        if (canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDirection = touchMovement.GetMovementInput().normalized;

        rb.velocity = dashDirection * dashingPower;
        tr.emitting = true;
        sr.color = new Color(1f, 1f, 1f, 0.4f);
        playerHealth.invulnerable = true;
        Debug.Log(dashDirection);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;
        tr.emitting = false;
        sr.color = new Color(1f, 1f, 1f, 1f);
        playerHealth.invulnerable = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimation()
    {
        anim.SetBool("idle", false);

        float moveX = moveVector.x;
        float moveY = moveVector.y;

        if (Mathf.Abs(moveX) >= Mathf.Abs(moveY))
        {
            if (moveX > 0 && animationIndex != 3)
            {
                anim.SetTrigger("walkRight");
                animationIndex = 3;
                lastAnimationIndex = animationIndex;


            }
            else if (moveX < 0 && animationIndex != 4)
            {
                anim.SetTrigger("walkLeft");
                animationIndex = 4;
                lastAnimationIndex = animationIndex;
            }
        }
        else if(Mathf.Abs(moveY) >= Mathf.Abs(moveX))
        {
            if (moveY > 0 && animationIndex != 1)
            {
                anim.SetTrigger("walkUp");
                animationIndex = 1;
                lastAnimationIndex = animationIndex;
            }
            else if (moveY < 0 && animationIndex != 2)
            {
                anim.SetTrigger("walkDown");
                animationIndex = 2;
                lastAnimationIndex = animationIndex;
            }
        }


    }
}
