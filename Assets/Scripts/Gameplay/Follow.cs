using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
        GameObject ghost;
        Rigidbody2D rb2d;
        float impulseForce;
        float homingDelay;
        Timer homingTimer;
        private Animator anim;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    /// 

        void Start()
        {
            // save values for efficiency
            ghost = GameObject.FindGameObjectWithTag("Player");
            homingDelay = 1;
            rb2d = GetComponent<Rigidbody2D>();
     
            // create and start timer
            homingTimer = gameObject.AddComponent<Timer>();
            homingTimer.Duration = homingDelay;
            homingTimer.AddTimerFinishedEventListener(HandleHomingTimerFinishedEvent);
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
            rb2d.AddForce(direction * impulseForce,
                ForceMode2D.Impulse);

            // restart timer
            homingTimer.Run();
        }
    }

