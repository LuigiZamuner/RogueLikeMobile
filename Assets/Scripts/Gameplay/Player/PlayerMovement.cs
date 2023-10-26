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
    private bool isDashing = false;
    private bool canDash = true;
    private float dashingPower = 15f; 
    private float dashDuration = 0.2f; 
    private float dashCooldown = 1.5f; 

    private void Awake()
    {

        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        tr= GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        playerHealth= GetComponent<Health>();

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
            rb.velocity = moveVector * moveSpeed;
        }
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
    private void OnDashStarted(InputAction.CallbackContext value)
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
        Vector2 dashDirection = lastMoveDirection.normalized;

        rb.velocity = dashDirection * dashingPower;
        tr.emitting = true;
        sr.color = new Color(1f, 1f, 1f, 0.4f);
        playerHealth.invulnerable = true;

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
