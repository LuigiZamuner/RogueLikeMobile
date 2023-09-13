using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    GameObject ghost;
    Rigidbody2D rb2d;
    private float impulseForce;
    private float homingDelay;
    Timer homingTimer;
    private Animator anim;
    public bool followOrOposite = true;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    /// 
    private void Awake()
    {
        homingTimer = gameObject.AddComponent<Timer>();
    }
    void Start()
    {
        // save values for efficiency
        ghost = GameObject.FindGameObjectWithTag("Player");
        homingDelay = 0.2f;
        rb2d = GetComponent<Rigidbody2D>();

        // create and start timer
        homingTimer.Duration = homingDelay;
        homingTimer.AddTimerFinishedEventListener(FollowBool);
        homingTimer.Run();
    }
 

    /// <summary>
    /// Sets the impulse force
    /// </summary>
    /// <value>impulse force</value>
    public void SetImpulseForce(float impulseForce)
    {
        this.impulseForce = impulseForce;
    }

    /// <summary>
    /// Handles the homing timer finished event
    /// </summary>
    void HandleHomingTimerFinishedEvent()
    {
        // stop moving
        rb2d.velocity = Vector2.zero;

        // calculate direction to burger and start moving toward it    
        Vector2 direction = (ghost.transform.position - transform.position).normalized;
        rb2d.AddForce(direction * impulseForce,ForceMode2D.Impulse);

        // restart timer
        homingTimer.Run();
    }
    void FollowOpposite()
    {
        rb2d.velocity = Vector2.zero;

        Vector2 direction = (transform.position - ghost.transform.position).normalized;
        rb2d.AddForce(direction * impulseForce, ForceMode2D.Impulse);

        homingTimer.Run();
    }
    public void FollowBool()
    {
        if (followOrOposite)
        {
            HandleHomingTimerFinishedEvent();
        }
        else
        {
            FollowOpposite();
        }
    }
}

