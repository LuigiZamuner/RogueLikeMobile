using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 joystickSize = new Vector2(100, 100);

    [SerializeField]
    private FloatingJoystick joystick;



    private Finger movementFinger;
    public Vector2 movementAmount;
    private Animator anim;
    private PlayerMovement pM;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    public void HandleFingerMove(Finger finger)
    {
        if (finger == movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = finger.currentTouch;
            if (Vector2.Distance(currentTouch.screenPosition, joystick.rectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - joystick.rectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - joystick.rectTransform.anchoredPosition;
            }
            joystick.knob.anchoredPosition = knobPosition;
            movementAmount = knobPosition / maxMovement;
        }
    }
    private void FixedUpdate()
    {

    }
    private void HandleLoseFinger(Finger finger)
    {
        if (finger == movementFinger)
        {
            movementFinger = null;
            joystick.knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            movementAmount = Vector2.zero;
            anim.SetBool("idle", true);
            pM.animationIndex = 0;
        }
    }

    private void HandleFingerDown(Finger finger)
    {
        if (movementFinger == null && finger.screenPosition.x <= Screen.width / 2f)
        {
            movementFinger = finger;
            movementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.rectTransform.sizeDelta = joystickSize;
            joystick.rectTransform.anchoredPosition = ClampStartPosition(finger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < joystickSize.x / 2)
        {
            startPosition.x = joystickSize.x / 2;
        }
        if (startPosition.y < joystickSize.y / 2)
        {
            startPosition.y = joystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - joystickSize.y / 2)
        {
            startPosition.y = Screen.height - joystickSize.y / 2;
        }

        return startPosition;
    }
    public Vector2 GetMovementInput()
    {
        return movementAmount;
    }
}